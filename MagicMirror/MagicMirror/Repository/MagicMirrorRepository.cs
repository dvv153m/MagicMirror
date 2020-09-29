using MagicMirror.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace MagicMirror.Repository
{
    public class MagicMirrorRepository
    {
        private List<Models.MagicMirror> _cache;
        private const string _key = "magicMirror";

        public MagicMirrorRepository()
        {            
            var magicMirrors = Preferences.Get(_key, "");
            if (!string.IsNullOrEmpty(magicMirrors))
            {
                _cache = JsonConvert.DeserializeObject<List<Models.MagicMirror>>(magicMirrors);
            }
            else
            {
                _cache = new List<Models.MagicMirror>();
            }
        }

        public void AddOrUpdate(Models.MagicMirror magicMirror)
        {
            //find magicMirror in _cache
            var mirror = _cache.Where(m => m.BleAddress == magicMirror.BleAddress).FirstOrDefault();
            if (mirror != null)
            {
                mirror.Ip = magicMirror.Ip;
                mirror.SelectedNetwork = magicMirror.SelectedNetwork;
            }
            else
            {
                _cache.Add(magicMirror);
            }
            
            string magicMirrorsJson = JsonConvert.SerializeObject(_cache);
            Preferences.Set(_key, magicMirrorsJson);
        }

        public List<Models.MagicMirror> GetAll()
        {
            return _cache;
        }

    }
}
