using Dapplo.Config.Language;

namespace Dapplo.ActiveDirectory.Finder.Configuration.Impl
{
    public class FinderTranslationsImpl : LanguageBase<IFinderTranslations>, IFinderTranslations
    {
        #region Implementation of IConfigTranslations

        public string Filter { get; }

        #endregion

        #region Implementation of ICoreTranslations

        public string Cancel { get; }
        public string Ok { get; }

        #endregion

        #region Implementation of IFinderTranslations

        public string Configuration { get; }
        public string Title { get; }

        #endregion
    }
}
