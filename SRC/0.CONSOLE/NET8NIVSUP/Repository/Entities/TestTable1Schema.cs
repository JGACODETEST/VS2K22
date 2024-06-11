using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET8NIVSUP.Repository.Entities
{
    [Table(name: "testtable1")]
    public class TestTable1Schema
    {
        [Column(name: "id")]
        public int Id { get; set; }

        [Column(name: "descripcion")]
        public string Descripcion { get; set; }
    }
}
