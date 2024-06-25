using Google.Protobuf.WellKnownTypes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace NET7.Service.Model.Dto
{
    public class TestTable1Dto
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        // Override the ToString method
        public override string ToString()
        {
            return $"TestTable1Dto >>  Id: {Id}, Descripcion: {Descripcion}";
        }
    }
}