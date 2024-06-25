using System.ComponentModel.DataAnnotations.Schema;

namespace NET472.Repository.Entities
{
    [Table(name: "TESTTABLE1")]
    public class TestTable1
    {
        [Column(name: "ID")]
        public int Id { get; set; }

        [Column(name: "DESCRIPCION")]
        public string Descripcion { get; set; }
    }
}