using Newtonsoft.Json;
using SlnGen.Core;
using SlnGen.Core.Utils;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace SlnGen.Core.Files
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

        public JsonFile WithProperty(string key, object value, params string[] path)
        {
            return WithProperty(new KeyValuePair<string, object>(key, value), path);
        }

        public JsonFile WithProperty(KeyValuePair<string, object> property, params string[] path)
        {
            return WithProperties(new List<KeyValuePair<string, object>> { property }, path);
        }

        public JsonFile WithProperties(Dictionary<string, object> properties, params string[] path)
        {
            var root = _contents;
            foreach (var pathPart in path)
            {
                if (!root.ContainsKey(pathPart))
                {
                    root.Add(pathPart, new OverwritableDictionary<string, object>());
                }
                root = (OverwritableDictionary<string, object>)root[pathPart];
            }

            foreach (var kvp in properties)
            {
                root.Add(kvp);
            }

            Build();
            return this;
        }

        public JsonFile WithProperties(ICollection<KeyValuePair<string, object>> properties, params string[] path)
        {
            var root = _contents;
            foreach (var pathPart in path)
            {
                if (!root.ContainsKey(pathPart))
                {
                    root.Add(pathPart, new OverwritableDictionary<string, object>());
                }
                root = (OverwritableDictionary<string, object>)root[pathPart];
            }


            foreach (var kvp in properties)
            {
                root.Add(kvp);
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
