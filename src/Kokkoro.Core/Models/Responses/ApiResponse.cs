using System;
using System.Collections.Generic;
using System.Text;

namespace Kokkoro.Core.Models;

public class ApiResponse<T>
{
    public int Code { get; set; }

    public string Message { get; set; } = string.Empty;

    public T? Data { get; set; }

    public bool Success => Code == 200;
}

//{
//  "code": 200,
//  "message": "成功",
//  "data": {
//    "totalCount": 100,
//    "items": []
//  }
//}