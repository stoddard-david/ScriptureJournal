using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptureJournal.Models
{
  public class Scripture
  {
    public int ID { get; set; }
    public string Book { get; set; }
    public int Chapter { get; set; }
    public int Verse { get; set; }
    public string Note { get; set; }
    public DateTime AddedDate { get; set; }
  }
}
