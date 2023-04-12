using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Financeira.Domain.Enum
{
    public enum TipoFinanciamento
    {
        [EnumMember(Value ="Direto")]
        CREDITO_DIRETO,
        [EnumMember(Value = "Consignado")]
        CREDITO_CONSIGNADO,
        [EnumMember(Value = "Pessoa Juridica")]
        CREDITO_PESSOA_JURIDICA,
        [EnumMember(Value = "Pessoa Fisica")]
        CREDITO_PESSOA_FISICA,
        [EnumMember(Value = "Imobiliario")]
        CREDITO_IMOBILIARIO,
    }
}
