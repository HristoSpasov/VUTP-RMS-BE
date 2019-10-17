namespace RMS.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using Enums;
    using RMS.Data.Contracts;

    public class SeedService : ISeedService
    {
        private readonly RMS_Db_Context dbContext;

        public SeedService(RMS_Db_Context dbContext)
        {
            this.dbContext = dbContext;
        }

        public void SeedAll()
        {
            this.SeedSpecialty();
            this.SeedDisciplines();
            this.SeedRooms();
            this.SeedTeachers();
            this.SeedSpecialtyDiscipline();
            this.SeedEvents();
            this.SeedRoomEvent();
            this.SeedTeacherEvent();
        }


        public ISeedService SeedSpecialty()
        {
            if (!this.dbContext.Specialities.Any())
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
                        Id = Guid.Parse("c359a916-c3c5-4505-93d0-61662abc508a"),
                        Name = SpecialtySeedDataEnum.KASP.ToString(),
                        Grade = 1
                    },
                    new Specialty()
                    {
                        Name = SpecialtySeedDataEnum.KASP.ToString(),
                        Grade = 3
                    }
                };

                this.dbContext.Specialities.AddRange(specialties);


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
                        LastName = "Stoiceva",
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
            throw new System.NotImplementedException();
        }

        public ISeedService SeedRoomEvent()
        {
            throw new System.NotImplementedException();
        }

        public ISeedService SeedTeacherEvent()
        {
            throw new System.NotImplementedException();
        }

        public ISeedService SeedEvents()
        {
            throw new System.NotImplementedException();
        }
    }
}