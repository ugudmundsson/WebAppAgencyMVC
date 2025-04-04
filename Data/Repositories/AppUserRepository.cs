using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;

public class AppUserRepository(AppDbContext context) : BaseRepository<AppUserEntity, AppUser>(context), IAppUserRepository
{
}


