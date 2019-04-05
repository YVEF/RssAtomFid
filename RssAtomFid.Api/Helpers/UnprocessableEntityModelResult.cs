using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.Helpers
{
    public class UnprocessableEntityModelResult : ObjectResult
    {
        public UnprocessableEntityModelResult(ModelStateDictionary state) : base(new SerializableError(state))
        {
            if (state == null) throw new NullReferenceException(nameof(state));
            StatusCode = 422;
        }
    }
}
