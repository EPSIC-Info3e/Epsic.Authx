using System;
using Epsic.Authx.Enums;
using Microsoft.AspNetCore.Identity;

namespace Epsic.Authx.Models
{
    public class TestCovid
    {
        public Guid Id { get; set; }
        public DateTime DateTest { get; set; }
        public bool Resultat { get; set; }
        public TypeTestCovid TypeDeTest { get; set; }
        public IdentityUser User { get; set; }
    }

    public class TestCovidDto
    {
        public DateTime DateTest { get; set; }
        public bool Resultat { get; set; }
        public TypeTestCovid TypeDeTest { get; set; }
        public Guid User { get; set; }
    }
}