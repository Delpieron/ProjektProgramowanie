using ProjektProgramowanie.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektProgramowanie
{
    /// <summary>
    /// Lista właściwości przedmiotów w tabeli
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public int NIP { get; set; }
        public DateTime BirthDate { get; set; }
        public string Name { get; set; }
        public string Surename { get; set; }
        public string Email { get; set; }

        [ForeignKey("PermissionFK")]
        public int PermissionsId { get; set; }

    }
}
