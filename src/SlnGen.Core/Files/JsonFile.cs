using Newtonsoft.Json;
using SlnGen.Core;
using SlnGen.Core.Utils;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace ZESoft.SlnGen.Core.Files
{
    public class JsonFile : ProjectFile
    {
        OverwritableDictionary<string, object> _contents;

        public JsonFile(string fileName, Dictionary<string, object> contents = null)
            : base($"{fileName}{(fileName.EndsWith(".json") ? String.Empty : ".json")}", false, true)
        {
            //_contents = (OverwritableDictionary<string, object>)(contents ?? new OverwritableDictionary<string, object>());
            _contents = new OverwritableDictionary<string, object>(contents);

            Build();
        }

        public JsonFile WithProperty(string key, object value)
        {
            return WithProperty(new KeyValuePair<string, object>(key, value));
        }

        public JsonFile WithProperty(KeyValuePair<string, object> property)
        {
            return WithProperties(new List<KeyValuePair<string, object>> { property });
        }

        public JsonFile WithProperties(Dictionary<string, object> properties)
        {
            foreach (var kvp in properties)
            {
                _contents.Add(kvp);
            }

            Build();
            return this;
        }

        public JsonFile WithProperties(ICollection<KeyValuePair<string, object>> properties)
        {
            foreach (var kvp in properties)
            {
                _contents.Add(kvp);
            }

            Build();
            return this;
        }

        void Build()
        {
            var eo = new ExpandoObject();
            var eoCollection = (ICollection<KeyValuePair<string, object>>)eo;

            foreach (var kvp in _contents)
            {
                eoCollection.Add(kvp);
            }

            FileContents = JsonConvert.SerializeObject(eoCollection);
        }
    }
}
