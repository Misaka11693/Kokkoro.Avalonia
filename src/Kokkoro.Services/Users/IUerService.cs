﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using Kokkoro.Core.Models;
using Kokkoro.Services.Users.Dtos;

namespace Kokkoro.Services.Users;

public interface IUserService
{
    Task<PageResponse<UserDto>> GetPageAsync(UserQueryDto query);

    Task<bool> ContainsCodeAsync(string code);

    Task AddAsync(UserDto user);

    Task UpdateAsync(UserDto user);

    Task DeleteAsync(string code);
}
