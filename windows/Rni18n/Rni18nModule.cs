using ReactNative.Bridge;
using System;
using System.Collections;
using System.Collections.Generic;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace Rni18n.Rni18n
{
    /// <summary>
    /// A module that allows JS to share data.
    /// </summary>
    class Rni18nModule : NativeModuleBase
    {
        /// <summary>
        /// Instantiates the <see cref="Rni18nModule"/>.
        /// </summary>
        internal Rni18nModule()
        {

        }

        /// <summary>
        /// The name of the native module.
        /// </summary>
        public override string Name
        {
            get
            {
                return "RNI18n";
            }
        }

        public override IReadOnlyDictionary<string, object> Constants
        {
            get
            {
                return new Dictionary<string, object>
                {
                    { "languages", this.GetLocaleList() }
                };
            }
        }

        private IList GetLocaleList()
        {
            var returnList = new List<string>();
            var langList = Windows.System.UserProfile.GlobalizationPreferences.Languages;
            var langListEnum = langList.GetEnumerator();
            while (langListEnum.MoveNext())
            {
                var curr = langListEnum.Current;
                returnList.Add(curr);
            }
            return returnList;
        }

        [ReactMethod]
        async void getLanguages(IPromise promise)
        {
            try
            {
                promise.Resolve(GetLocaleList());
            }
            catch (Exception e)
            {
                promise.Reject(e);
            }
        }
    }
}
