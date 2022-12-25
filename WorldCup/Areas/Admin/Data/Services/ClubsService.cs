﻿using WorldCup.Areas.Admin.Models;
using WorldCup.Data;
using WorldCup.Data.Base;
using WorldCup.Data.Services;
using WorldCup.Models;

namespace WorldCup.Areas.Admin.Data.Services
{
	public class ClubsService : EntityBaseRepository<Club>, IClubsService
	{
		public ClubsService(AppDbContext context) : base(context) { }
	}
}
