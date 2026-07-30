# PageView 分页容器 — 设计说明

> **状态：V1.0 已实现**（`Controls/PageView.*`）  
> 归档对照：`Backups/PagedDataGrid-2026-06-08/`（现行 `PagedDataGrid` 快照）  
> 命名：控件名 **`PageView`**（讨论中偶写 `PagedView`，以 `PageView` 为准）

---

## V1.0 定稿结论

设计成熟度：**约 95%**。剩余优化（如 `DataGridSelectionBehavior`）属**后续增强**，**不阻塞** `PageView` 落地。

`PageView` 第一版只做：

```text
内容容器 + 统计栏 + Ursa 分页器
```

---

## 1. 背景

Kokkoro 企业后台中，大量页面具备相同外壳：

- 上方：列表/卡片等内容（`DataGrid`、`TreeDataGrid`、`ListBox`、`ItemsRepeater`、`CardView` 等）
- 下方：数据统计 + Ursa 分页器

需要 **通用分页容器**，而不是针对某一种列表控件的 `PagedDataGrid`。

---

## 2. 设计目标

### PageView 负责

- 承载任意子内容（`ContentPresenter`）
- 统一底栏：共 N 条、（可选）已选 N 条
- 统一 Ursa `Pagination`

### PageView 不负责

- `DataGrid` / `TreeDataGrid` 等业务能力
- **`SelectedItems` / `SelectedUsers` / 选中集合的维护**
- **`SelectedCount` 的计算与监听**
- 数据查询、分页数据切片（由 ViewModel 完成）

---

## 3. 为什么不做成 PagedDataGrid

```xml
<!-- 不推荐：每种列表一个包装控件 -->
<PagedDataGrid />
<PagedTreeDataGrid />
<PagedListBox />
```

推荐：

```xml
<PageView ...>
    <DataGrid />
</PageView>

<PageView ...>
    <TreeDataGrid />
</PageView>
```

避免重复实现底栏与分页逻辑。

---

## 4. 布局结构

```text
┌──────────────────────────────────────┐
│              Content                 │
│   (DataGrid / ListBox / …)           │
├──────────────────────────────────────┤
│ 共 15 条    已选 1 条      Pagination │
└──────────────────────────────────────┘
```

内部：

```text
PageView
├── ContentPresenter
└── FooterBar
    ├── StatisticsArea（TotalCount / SelectedCount）
    └── Ursa Pagination（固定，不可替换）
```

实现时注意（与现行 `PagedDataGrid` 一致）：

- 根与 `ContentPresenter`：`MinHeight="0"`，拉伸填满 Dock 文档区
- 底栏 `ClipToBounds="False"`，避免 PageSize 下拉被裁切
- 窄屏时底栏可横向滚动

### 4.1 样式约定（V1 必须与现网一致）

**`PageView` 不另起一套视觉**；底栏与分页区 **对照 `Controls/PagedDataGrid.axaml` 原样沿用** Semi + Ursa 现有写法。

| 区域 | 约定 |
|------|------|
| 底栏容器 | `Border`：`Padding="8,6"`，`Background="{DynamicResource SemiColorFill0}"`，`BorderBrush="{DynamicResource SemiColorBorder}"`，`BorderThickness="0,1,0,0"`，`ClipToBounds="False"` |
| 底栏滚动 | 外层 `ScrollViewer`：`HorizontalScrollBarVisibility="Auto"`，`VerticalScrollBarVisibility="Disabled"`，`ClipToBounds="False"` |
| 底栏布局 | `Grid`：`ColumnDefinitions="Auto,*,Auto"`，`ColumnSpacing="12"` |
| 统计文字 | `TextBlock`：`Foreground="{DynamicResource SemiColorText2}"`；左侧 `StackPanel` 横向 `Spacing="16"`；文案 `共 {0} 条` / `已选 {0} 条` |
| 分页器 | Ursa `Pagination`：`VerticalAlignment="Center"`；属性绑定到 `PageView` 根（`ElementName=Root`），与现 `PagedDataGrid` 相同 |
| 控件根 | `MinHeight="0"` `MinWidth="0"` `HorizontalAlignment="Stretch"` `VerticalAlignment="Stretch"` |

内容区（`ContentPresenter`）**不加**底栏那层 `Border` 背景；列表外层卡片边框仍由 **页面** 决定（如 Users 页的 `SemiColorBg0` + 圆角 `Border`），与现结构一致。

实现时以 `PagedDataGrid.axaml` 与备份 `Backups/PagedDataGrid-2026-06-08/PagedDataGrid.axaml` 为 **样式对照源**，避免引入新 DynamicResource 或自定义 Footer 主题。

---

## 5. V1.0 属性清单（定稿）

| 属性 | 绑定模式 | 默认值 / 说明 |
|------|----------|----------------|
| `CurrentPage` | **TwoWay** | 当前页码；与 Ursa、现有 VM 字段统一（**不用 `PageIndex`**） |
| `PageSize` | **TwoWay** | 每页条数；变更时控件内 **`CurrentPage = 1`**（见 §7） |
| `TotalCount` | **OneWay** | 总条数；仅 VM → View，分页器不反写 |
| `SelectedCount` | **OneWay** | 已选条数；**仅展示**，见 §6 |
| `ShowSelectedCount` | — | 默认 **`false`** |
| `ShowQuickJumper` | — | 是否显示快速跳转 |
| `ShowPageSizeSelector` | — | 是否显示每页条数选择 |
| `PageSizeOptions` | — | 如 `5, 10, 20, 50` |
| `DisplayCurrentPageInQuickJumper` | — | 快速跳转是否展示当前页 |
| `IsPagingEnabled` | **OneWay** | 默认 `true`；加载中绑 `!IsLoading`（见 §7） |

### 典型绑定

```xml
<PageView
    CurrentPage="{Binding CurrentPage, Mode=TwoWay}"
    PageSize="{Binding PageSize, Mode=TwoWay}"
    TotalCount="{Binding TotalCount}"
    SelectedCount="{Binding SelectedCount}"
    ShowSelectedCount="True"
    IsPagingEnabled="{Binding !IsLoading}">

    <DataGrid />

</PageView>
```

### V1.0 明确不做的 API

以下**全部不在第一版** `PageView` 中提供：

```text
SelectedItems
SelectedUsers
SelectionMode
Columns
ItemsSource
FooterTemplate
PaginationTemplate
CustomPagination
```

不暴露 Ursa `Pagination` 的全部属性，不提供可替换分页器模板。

---

## 6. SelectedCount 约定（最高优先级 · 写死）

> 若不写死，使用者一定会问：  
> `<PageView SelectedCount="{Binding SelectedCount}" />` **为什么不会自动变？**

### 6.1 框架约定（必须遵守）

```text
PageView 不负责管理 SelectedItems。

PageView 的 SelectedCount 仅用于显示。

SelectedCount 由页面自行维护并绑定。

推荐通过：
- DataGrid.SelectionChanged（View 层）
- DataGridSelectionBehavior（后续增强，可选）

同步更新到 ViewModel。
```

### 6.2 职责边界

```text
PageView      → 只显示 SelectedCount
内容控件       → 只负责选择（DataGrid.SelectedItems 等）
ViewModel     → 只存状态（SelectedCount、SelectedUsers 等）
```

### 6.3 为什么不让 PageView 自动统计

`Content` 可能是 `DataGrid`、`TreeDataGrid`、`ListBox`、`ItemsRepeater`、`CardView` 等。  
`ItemsRepeater` / 部分 `CardView` **没有** `SelectedItems` / `SelectionChanged`。

若在 `PageView` 内 `Content as DataGrid` 并监听选中：

- 与具体控件耦合
- 无法覆盖全部内容类型
- 违反单一职责

**禁止**在 `PageView` 内 `FindDataGrid` / 反射监听选中。

### 6.4 推荐同步方式

#### 方式一：View 层（V1 推荐）

```csharp
private void OnGridSelectionChanged(object? sender, SelectionChangedEventArgs e)
{
    if (DataContext is UsersPageViewModel vm)
        vm.SelectedCount = UsersGrid.SelectedItems?.Count ?? 0;
}
```

优点：简单、直观、性能最好。

#### 方式二：`DataGridSelectionBehavior`（后续增强，不阻塞 PageView）

```xml
<DataGrid behaviors:DataGridSelectionBehavior.SelectedCount="{Binding SelectedCount, Mode=TwoWay}" />
```

优点：MVVM 更纯、可复用。  
缺点：实现与理解成本更高；**V1 不强制提供**，属框架后续增强。

### 6.5 与 SelectedUsers 的关系

| 字段 | 用途 | 由谁维护 |
|------|------|----------|
| `SelectedCount` | 底栏「已选 N 条」 | 页面 / VM |
| `SelectedUsers` | 命令 `CanExecute`、业务逻辑 | 页面 / VM（**不在 PageView**） |

二者可在同一次 `SelectionChanged` 中一并更新。详见 Users 页与 `Backups/PagedDataGrid-2026-06-08/README.md`。

---

## 7. 控件内置行为（V1 必须实现）

### 7.1 `IsPagingEnabled`

加载或查询进行中应禁用分页交互，避免连续触发请求：

```xml
<PageView IsPagingEnabled="{Binding !IsLoading}" />
```

`IsPagingEnabled == false` 时，底栏 Ursa `Pagination` 应整体不可操作，包括但不限于：

- 上一页 / 下一页
- 修改 `PageSize`
- 快速跳转

避免出现：

```text
查询中 → 又点下一页 → 又改 PageSize → 连续触发查询
```

### 7.2 `PageSize` 变更自动回第一页

**在 `PageView` 内统一实现**，不要求每个页面 VM 写：

```csharp
partial void OnPageSizeChanged(...) { CurrentPage = 1; }
```

行为：

```text
PageSize 变化
    ↓
CurrentPage = 1
    ↓
（通过 TwoWay 绑定通知 VM；若已在第 1 页，需保证绑定仍能触发分页刷新，实现时参考现行 PagedDataGrid 的 0→1 脉冲）
```

---

## 8. 命名约定

全框架统一使用 **`CurrentPage`**，不使用 `PageIndex`：

- 与现有 VM：`CurrentPage` / `PageSize` / `TotalCount` 一致
- 与 Ursa `Pagination.CurrentPage` 一致
- 避免 VM 与控件字段名不一致、到处转换

`TotalCount` 注册为 **默认 `BindingMode.OneWay`**：数据流仅为 VM → View。

---

## 9. Footer 显示逻辑

始终显示：

```text
共 {TotalCount} 条
```

当 `ShowSelectedCount == true` 时追加：

```text
已选 {SelectedCount} 条
```

当 `ShowSelectedCount == false`（**默认**）时，不显示已选行。

---

## 10. 分页器

- **固定**使用 Ursa `Pagination`
- 不提供 `PaginationTemplate`、`CustomPagination`、子级 `PageView.Pagination` 等扩展点
- 仅通过 §5 所列属性配置常用行为

---

## 11. 从 PagedDataGrid 迁移（规划）

| 现在（PagedDataGrid） | 迁移后（PageView） |
|----------------------|-------------------|
| 控件内嵌 `PART_DataGrid` | `PageView` + 子级 `DataGrid` |
| `SelectedItems` OneWayToSource | **页面层** View / Behavior，不在 PageView |
| `SelectAllCurrentPage()` | View 调 `DataGrid` 或独立 Helper |
| 底栏 + 分页 + PageSize 回 1 | 全部由 `PageView` 提供 |

---

## 12. V1.0 实现检查清单

- [x] `PageView.axaml` / `PageView.axaml.cs`
- [x] §5 全部属性与默认绑定模式
- [x] `IsPagingEnabled` 禁用 Ursa 分页器全部交互
- [x] `PageSize` 变更 → `CurrentPage = 1`（含已在第 1 页时的绑定脉冲）
- [x] **不**监听子控件 `SelectionChanged`
- [x] **不**实现 §5「V1.0 明确不做的 API」
- [x] Users 页已迁移为 `PageView` + `DataGrid`

### 后续增强（不阻塞 V1）

- [ ] `DataGridSelectionBehavior`（`SelectedCount` / `SelectedUsers` 同步）
- [ ] 其他列表控件的选中 Behavior

---

## 13. 一句话总结

**PageView V1 = 内容容器 + 统计栏 + Ursa 分页器；`SelectedCount` 只显示、由页面维护；`IsPagingEnabled` 与 `PageSize→回第一页` 由控件内置。**
