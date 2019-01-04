using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace UrlShortener.WebApi.Exceptions {
    public class UrlNotFoundException : Exception {
        public UrlNotFoundException() {
        }

        public UrlNotFoundException(string message) : base(message) {
        }

        public UrlNotFoundException(string message, Exception innerException) : base(message, innerException) {
        }

        protected UrlNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
