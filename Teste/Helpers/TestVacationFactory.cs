using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationAPI.Models;

namespace Teste.Helpers
{
    public class TestVacationFactory
    {
            public static Vacation CreateVacation(int id)
            {
                return new Vacation
                {
                    Id = id,
                    Destination = "test"+id,
                    duration = id,
                    Price = id *5
                };
            }

            public static List<Vacation> CreateVacations(int count)
            {
                List<Vacation> vacation = new List<Vacation>();
                for (int i = 1; i <= count; i++)
                {
                    vacation.Add(CreateVacation(i));
                }

                return vacation;
            }

        
    }
}
