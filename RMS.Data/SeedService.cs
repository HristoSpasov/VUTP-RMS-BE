namespace RMS.Data
{
    using Entities;
    using Enums;
    using Microsoft.AspNetCore.Identity;
    using RMS.Data.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SeedService : ISeedService
    {
        private readonly RMS_Db_Context dbContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public SeedService(RMS_Db_Context dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void SeedAll()
        {
            //this.SeedSpecialty();
            //this.SeedDisciplines();
            //this.SeedRooms();
            //this.SeedTeachers();
            //this.SeedSpecialtyDiscipline();
            //this.SeedEvents();
            this.SeedUsersAndRoles().GetAwaiter().GetResult();
        }

        public ISeedService SeedSpecialty()
        {
            if (!this.dbContext.Specialties.Any())
            {
                var specialties = new List<Specialty>()
                {
                    new Specialty()
                    {
                        Id = Guid.Parse("2c89f567-2cfd-4609-895c-fa2aec7f0cb1"),
                        Name = SpecialtySeedDataEnum.Software_Design.ToString(),
                        Grade = 1
                    },
                    new Specialty()
                    {
                        Id = Guid.Parse("808e698d-087b-46f3-a9d4-b739d2e105f4"),
                        Name = SpecialtySeedDataEnum.Software_Design.ToString(),
                        Grade = 2
                    },
                    new Specialty()
                    {
                        Id = Guid.Parse("6a96cd00-5862-470d-bcba-efe577079ccd"),
                        Name = SpecialtySeedDataEnum.Software_Design.ToString(),
                        Grade = 3
                    },
                    new Specialty()
                    {
                        Id = Guid.Parse("c359a916-c3c5-4505-93d0-61662abc508a"),
                        Name = SpecialtySeedDataEnum.KASP.ToString(),
                        Grade = 1
                    },
                    new Specialty()
                    {
                        Id = Guid.Parse("d1cfc921-e4eb-4955-8b57-a5beb2be028b"),
                        Name = SpecialtySeedDataEnum.KASP.ToString(),
                        Grade = 2
                    },
                    new Specialty()
                    {
                        Name = SpecialtySeedDataEnum.KASP.ToString(),
                        Grade = 3
                    }
                };

                this.dbContext.Specialties.AddRange(specialties);

                this.dbContext.SaveChanges();
            }

            return this;
        }

        public ISeedService SeedDisciplines()
        {
            if (!this.dbContext.Disciplines.Any())
            {
                var disciplines = new List<Discipline>()
                {
                    new Discipline()
                    {
                        Id = Guid.Parse("a95d07aa-ea6b-4df7-84c3-b7a03467f4c5"),
                        Name = DisciplineSeedDataEnum.Maths_1.ToString()
                    },
                    new Discipline()
                    {
                        Id = Guid.Parse("aa5de6e3-aad3-4517-91fc-a207cf6ef087"),
                        Name = DisciplineSeedDataEnum.Maths_2.ToString()
                    },
                    new Discipline()
                    {
                        Id = Guid.Parse("009b5f2a-dc04-471d-a0ee-7f9b04989a2b"),
                        Name = DisciplineSeedDataEnum.OOP.ToString()
                    },
                    new Discipline()
                    {
                        Id = Guid.Parse("15b4ccf6-5599-429a-93ab-ff48a43da6e9"),
                        Name = DisciplineSeedDataEnum.Operating_Systems.ToString()
                    },
                    new Discipline()
                    {
                        Id = Guid.Parse("57908f04-cf54-4597-ae68-b9137cd18377"),
                        Name = DisciplineSeedDataEnum.LinuxAdministration.ToString()
                    }
                };

                this.dbContext.Disciplines.AddRange(disciplines);

                this.dbContext.SaveChanges();
            }

            return this;
        }

        public ISeedService SeedRooms()
        {
            if (!this.dbContext.Rooms.Any())
            {
                var rooms = new List<Room>()
                {
                    new Room()
                    {
                        Id = Guid.Parse("f5edeaeb-1ccb-4cba-ab3e-7a6c5f6b7894"),
                        Number = "120"
                    },
                    new Room()
                    {
                        Id = Guid.Parse("96f7d715-abe1-4157-9f61-c7ffcda142e2"),
                        Number = "220"
                    },
                    new Room()
                    {
                        Id = Guid.Parse("f97eda57-616b-47d5-89db-49d6cce78da7"),
                        Number = "318"
                    },
                    new Room()
                    {
                        Id = Guid.Parse("9ed1189c-d5c6-44ef-a182-7305a9919167"),
                        Number = "1005"
                    },
                };

                this.dbContext.Rooms.AddRange(rooms);

                this.dbContext.SaveChanges();
            }

            return this;
        }

        public ISeedService SeedTeachers()
        {
            if (!this.dbContext.Teachers.Any())
            {
                var teachers = new List<Teacher>()
                {
                    new Teacher()
                    {
                        Id = Guid.Parse("f735a46a-350a-447b-b39d-26dd69c5ff49"),
                        FirstName = "Nikolaj",
                        LastName = "Hristoskov",
                        AcademicTitle = "Docent"
                    },
                    new Teacher()
                    {
                        Id = Guid.Parse("50c4939b-8225-4f6c-a0ad-2ce2cd24edfb"),
                        FirstName = "Luybena",
                        LastName = "Nacheva",
                        AcademicTitle = "Professor"
                    },new Teacher()
                    {
                        Id = Guid.Parse("b1b62a86-6fc0-451b-a05d-b753d7955a1e"),
                        FirstName = "Merry",
                        LastName = "Dimitrova",
                        AcademicTitle = "DTN"
                    },new Teacher()
                    {
                        Id = Guid.Parse("247f8591-7a2e-424b-ad38-f1a4b8f06cac"),
                        FirstName = "Hristo",
                        LastName = "Spasov",
                        AcademicTitle = "0"
                    },
                };

                this.dbContext.Teachers.AddRange(teachers);

                this.dbContext.SaveChanges();
            }

            return this;
        }

        public ISeedService SeedSpecialtyDiscipline()
        {
            if (!this.dbContext.SpecialtiesDisciplines.Any())
            {
                var oopDiscipline = this.dbContext.Disciplines.FirstOrDefault(e => e.Name == DisciplineSeedDataEnum.OOP.ToString());
                var mathOneDiscipline = this.dbContext.Disciplines.FirstOrDefault(e => e.Name == DisciplineSeedDataEnum.Maths_1.ToString());
                var mathTwoDiscipline = this.dbContext.Disciplines.FirstOrDefault(e => e.Name == DisciplineSeedDataEnum.Maths_2.ToString());
                var operatingSystemDiscipline = this.dbContext.Disciplines.FirstOrDefault(e => e.Name == DisciplineSeedDataEnum.Operating_Systems.ToString());
                var linuxAdministrationDiscipline = this.dbContext.Disciplines.FirstOrDefault(e => e.Name == DisciplineSeedDataEnum.LinuxAdministration.ToString());

                var kaspSpecialtyFirstYear = this.dbContext.Specialties.FirstOrDefault(e => e.Name == SpecialtySeedDataEnum.KASP.ToString() && e.Grade == 1);
                var kaspSpecialtySecondYear = this.dbContext.Specialties.FirstOrDefault(e => e.Name == SpecialtySeedDataEnum.KASP.ToString() && e.Grade == 2);
                var kaspSpecialtyThirdYear = this.dbContext.Specialties.FirstOrDefault(e => e.Name == SpecialtySeedDataEnum.KASP.ToString() && e.Grade == 3);
                var softwareDesignSpecialtyFirstYear = this.dbContext.Specialties.FirstOrDefault(e => e.Name == SpecialtySeedDataEnum.Software_Design.ToString() && e.Grade == 1);
                var softwareDesignSpecialtySecondYear = this.dbContext.Specialties.FirstOrDefault(e => e.Name == SpecialtySeedDataEnum.Software_Design.ToString() && e.Grade == 2);
                var softwareDesignSpecialtyThirdYear = this.dbContext.Specialties.FirstOrDefault(e => e.Name == SpecialtySeedDataEnum.Software_Design.ToString() && e.Grade == 3);

                var specialtiesDisciplines = new List<SpecialtyDiscipline>()
                {
                    new SpecialtyDiscipline()
                    {
                        Id = Guid.Parse("403f3474-2c7a-4621-9bc0-5140af44abcc"),
                        Specialty = kaspSpecialtyFirstYear,
                        Discipline = mathOneDiscipline,
                    },
                    new SpecialtyDiscipline()
                    {
                        Id = Guid.Parse("cb0d38ab-c667-48b1-852e-94ba46d887df"),
                        Specialty = kaspSpecialtyFirstYear,
                        Discipline = mathTwoDiscipline,
                    },
                    new SpecialtyDiscipline()
                    {
                        Id = Guid.Parse("7f0c8f1b-5dd3-4a01-9f22-664fb899a8ea"),
                        Specialty = kaspSpecialtyFirstYear,
                        Discipline = operatingSystemDiscipline,
                    },
                    new SpecialtyDiscipline()
                    {
                        Id = Guid.Parse("c81d2296-447b-4d4f-b77a-7d0dff35f17a"),
                        Specialty = kaspSpecialtySecondYear,
                        Discipline = linuxAdministrationDiscipline,
                    },
                    new SpecialtyDiscipline()
                    {
                        Id = Guid.Parse("915312ee-1aee-4fac-8dfe-873833d50782"),
                        Specialty = softwareDesignSpecialtyFirstYear,
                        Discipline = mathOneDiscipline,
                    },
                    new SpecialtyDiscipline()
                    {
                        Id = Guid.Parse("c752ceb7-570f-40b6-a12d-9c0fd52a3d14"),
                        Specialty = softwareDesignSpecialtyFirstYear,
                        Discipline = mathTwoDiscipline,
                    },
                    new SpecialtyDiscipline()
                    {
                        Id = Guid.Parse("5c7fc1c5-dbcf-4d4e-90ee-3e5f759d760e"),
                        Specialty = softwareDesignSpecialtyFirstYear,
                        Discipline = operatingSystemDiscipline,
                    },
                    new SpecialtyDiscipline()
                    {
                        Id = Guid.Parse("3b7147ec-45cf-40ab-8f0d-3d4edc34aa7c"),
                        Specialty = softwareDesignSpecialtyThirdYear,
                        Discipline = oopDiscipline,
                    },
                    new SpecialtyDiscipline()
                    {
                        Id = Guid.Parse("83242702-c37b-4c39-aa0b-a8e3030277b5"),
                        Specialty = softwareDesignSpecialtySecondYear,
                        Discipline = linuxAdministrationDiscipline,
                    }
                };

                this.dbContext.SpecialtiesDisciplines.AddRange(specialtiesDisciplines);

                this.dbContext.SaveChanges();
            }

            return this;
        }

        public ISeedService SeedEvents()
        {
            if (!this.dbContext.Events.Any())
            {
                var oopDiscipline = this.dbContext.Disciplines.FirstOrDefault(e => e.Name == DisciplineSeedDataEnum.OOP.ToString());
                var mathOneDiscipline = this.dbContext.Disciplines.FirstOrDefault(e => e.Name == DisciplineSeedDataEnum.Maths_1.ToString());
                var mathTwoDiscipline = this.dbContext.Disciplines.FirstOrDefault(e => e.Name == DisciplineSeedDataEnum.Maths_2.ToString());
                var operatingSystemDiscipline = this.dbContext.Disciplines.FirstOrDefault(e => e.Name == DisciplineSeedDataEnum.Operating_Systems.ToString());
                var linuxAdministrationDiscipline = this.dbContext.Disciplines.FirstOrDefault(e => e.Name == DisciplineSeedDataEnum.LinuxAdministration.ToString());

                var kaspSpecialtyFirstYear = this.dbContext.Specialties.FirstOrDefault(e => e.Name == SpecialtySeedDataEnum.KASP.ToString() && e.Grade == 1);
                var kaspSpecialtySecondYear = this.dbContext.Specialties.FirstOrDefault(e => e.Name == SpecialtySeedDataEnum.KASP.ToString() && e.Grade == 2);
                var kaspSpecialtyThirdYear = this.dbContext.Specialties.FirstOrDefault(e => e.Name == SpecialtySeedDataEnum.KASP.ToString() && e.Grade == 3);
                var softwareDesignSpecialtyFirstYear = this.dbContext.Specialties.FirstOrDefault(e => e.Name == SpecialtySeedDataEnum.Software_Design.ToString() && e.Grade == 1);
                var softwareDesignSpecialtySecondYear = this.dbContext.Specialties.FirstOrDefault(e => e.Name == SpecialtySeedDataEnum.Software_Design.ToString() && e.Grade == 2);
                var softwareDesignSpecialtyThirdYear = this.dbContext.Specialties.FirstOrDefault(e => e.Name == SpecialtySeedDataEnum.Software_Design.ToString() && e.Grade == 3);

                var events = new List<Event>()
                {
                    //new Event()
                    //{
                    //    Room = this.dbContext.Rooms.FirstOrDefault(r => r.Number == "120"),
                    //    Discipline = linuxAdministrationDiscipline,
                    //    Teacher = this.dbContext.Teachers.FirstOrDefault(n => n.FirstName == "Hristo"),
                    //    EventSpecialties = new List<SpecialtyEvent>()
                    //    {
                    //        new SpecialtyEvent()
                    //        {
                    //            Specialty = kaspSpecialtyFirstYear
                    //        },
                    //        new SpecialtyEvent()
                    //        {
                    //            Specialty = softwareDesignSpecialtyFirstYear
                    //        }
                    //    },
                    //    StartTime = DateTime.UtcNow,
                    //    EndTime = DateTime.UtcNow.AddMinutes(45),
                    //},
                    //new Event()
                    //{
                    //    Room = this.dbContext.Rooms.FirstOrDefault(r => r.Number == "220"),
                    //    Discipline = oopDiscipline,
                    //    Teacher = this.dbContext.Teachers.FirstOrDefault(n => n.FirstName == "Nikolaj"),
                    //    EventSpecialties = new List<SpecialtyEvent>()
                    //    {
                    //        new SpecialtyEvent()
                    //        {
                    //            Specialty = softwareDesignSpecialtyThirdYear
                    //        }
                    //    },
                    //    StartTime = DateTime.UtcNow.AddDays(1),
                    //    EndTime = DateTime.UtcNow.AddDays(1).AddMinutes(45),
                    //},
                    //new Event()
                    //{
                    //    Room = this.dbContext.Rooms.FirstOrDefault(r => r.Number == "318"),
                    //    Discipline = mathOneDiscipline,
                    //    Teacher = this.dbContext.Teachers.FirstOrDefault(n => n.FirstName == "Merry"),
                    //    EventSpecialties = new List<SpecialtyEvent>()
                    //    {
                    //        new SpecialtyEvent()
                    //        {
                    //            Specialty = softwareDesignSpecialtyFirstYear
                    //        }
                    //    },
                    //    StartTime = DateTime.UtcNow.AddDays(3),
                    //    EndTime = DateTime.UtcNow.AddDays(3).AddMinutes(120),
                    //},
                    //new Event()
                    //{
                    //    Room = this.dbContext.Rooms.FirstOrDefault(r => r.Number == "318"),
                    //    Discipline = mathTwoDiscipline,
                    //    Teacher = this.dbContext.Teachers.FirstOrDefault(n => n.FirstName == "Merry"),
                    //    EventSpecialties = new List<SpecialtyEvent>()
                    //    {
                    //        new SpecialtyEvent()
                    //        {
                    //            Specialty = softwareDesignSpecialtySecondYear
                    //        }
                    //    },
                    //    StartTime = DateTime.UtcNow.AddDays(-3),
                    //    EndTime = DateTime.UtcNow.AddDays(-3).AddMinutes(120),
                    //},
                    //new Event()
                    //{
                    //    Room = this.dbContext.Rooms.FirstOrDefault(r => r.Number == "1005"),
                    //    Discipline = operatingSystemDiscipline,
                    //    Teacher = this.dbContext.Teachers.FirstOrDefault(n => n.FirstName == "Luybena"),
                    //    EventSpecialties = new List<SpecialtyEvent>()
                    //    {
                    //        new SpecialtyEvent()
                    //        {
                    //            Specialty = softwareDesignSpecialtySecondYear
                    //        },
                    //        new SpecialtyEvent()
                    //        {
                    //            Specialty = kaspSpecialtySecondYear
                    //        }
                    //    },
                    //    StartTime = DateTime.UtcNow.AddDays(5),
                    //    EndTime = DateTime.UtcNow.AddDays(5).AddMinutes(240),
                    //},
                };

                this.dbContext.Events.AddRange(events);

                this.dbContext.SaveChanges();
            }

            return this;
        }

        public async Task<ISeedService> SeedUsersAndRoles()
        {
            var roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await this.roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }
            }

            // Add admin user
            var adminPassword = "123456";
            var adminUser = new User
            {
                UserName = "admin",
                Email = "admin@utp.bg"
            };

            var adminUserExists = await this.userManager.FindByNameAsync(adminUser.UserName) != null;
            
            if (!adminUserExists)
            {
                var createAdminUserResult = await this.userManager.CreateAsync(adminUser, adminPassword);

                if (createAdminUserResult.Succeeded)
                {
                    await this.userManager.AddToRolesAsync(adminUser, roles);
                }
            }

            return await Task.FromResult<ISeedService>(this);
        }
    }
}