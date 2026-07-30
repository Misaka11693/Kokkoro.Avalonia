﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿namespace Kokkoro.Services.Users.Dtos;

public class UserQueryDto
{
    public string? Code { get; set; }

    public string? Name { get; set; }

    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}
