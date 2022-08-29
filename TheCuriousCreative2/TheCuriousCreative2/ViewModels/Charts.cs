using System;
using TheCuriousCreative2.Models;

namespace TheCuriousCreative2.ViewModels
{
	public class Charts
	{
        public List<Person> Data { get; set; }

        public Charts()
        {
            Data = new List<Person>()
        {
            new Person { Name = "David", Height = 170 },
            new Person { Name = "Michael", Height = 96 },
            new Person { Name = "Steve", Height = 65 },
            new Person { Name = "Joel", Height = 182 },
            new Person { Name = "Bob", Height = 134 }
        };
        }
    }
}

