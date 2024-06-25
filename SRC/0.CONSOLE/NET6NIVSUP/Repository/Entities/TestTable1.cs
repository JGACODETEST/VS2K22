using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET6NIVSUP.Repository.Entities
{
    [Table(name:"TESTTABLE1")]
    public class TestTable1
    {
        [Column(name:"ID")]
        public int Id { get; set; }

        [Column(name: "DESCRIPCION")]
        public string Descripcion { get; set; }
    }
}
