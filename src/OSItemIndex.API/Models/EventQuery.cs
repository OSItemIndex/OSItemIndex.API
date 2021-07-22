using Microsoft.AspNetCore.Mvc;
using OSItemIndex.API.Utils;
using OSItemIndex.Data;

namespace OSItemIndex.API.Models
{
    public class EventQuery
    {
        [ModelBinder(BinderType = typeof(JsonValueBinder))]
        public EventSource? Source { get; set; }

        [ModelBinder(BinderType = typeof(JsonValueBinder))]
        public EventType? Type { get; set; }
    }
}
