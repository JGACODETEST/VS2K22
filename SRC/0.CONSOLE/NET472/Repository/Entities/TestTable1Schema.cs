using System.ComponentModel.DataAnnotations.Schema;

namespace NET472.Repository.Entities
{
    [Table(name: "public.testtable1")]
    public class TestTable1Schema
    {
        [Column(name: "id")]
        public int Id { get; set; }

        [Column(name: "descripcion")]
        public string Descripcion { get; set; }
    }
}