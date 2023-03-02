using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FitnessClubRasima.DB;
using FitnessClubRasima.Windows;

namespace FitnessClubRasima.ClassHelper
{
    public class EFClass
    {
        public static FitnessClubRasimaEntities1 context { get; set; } = new FitnessClubRasimaEntities1();
    }
}
