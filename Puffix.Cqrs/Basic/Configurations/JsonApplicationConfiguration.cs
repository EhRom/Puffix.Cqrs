using Puffix.Cqrs.Configurations;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Puffix.Cqrs.Basic.Configurations
{
    /// <summary>
    /// Conteneur de configuration pour des paramètres stockés au format JSON.
    /// </summary>
    public class JsonApplicationConfiguration : ApplicationConfiguration
    {
        /// <summary>
        /// S'assure que la configuration est chargée.
        /// </summary>
        public override void EnsureIsLoaded()
        {
            if (parameters == null)
            {
                string loadedContent;
                if (configurationFileLoader != null)
                {
                    using Stream stream = configurationFileLoader(configurationFile);
                    using StreamReader reader = new StreamReader(stream);

                    // Appel synchrone, car l'appel asynchrone peut ne pas aboutir.
                    loadedContent = reader.ReadToEnd();
                }
                else
                {
                    using StreamReader reader = File.OpenText(configurationFile);
                    loadedContent = reader.ReadToEnd();
                }

                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    AllowTrailingCommas = true,
                };
                ApplicationConfigurationModel baseConfiguration = JsonSerializer.Deserialize<ApplicationConfigurationModel>(loadedContent, options);
                loadedContent = null;

                foreach (ConfigurationElement parameter in baseConfiguration.Parameters)
                {
                    if (parameter != null && parameter.Value is JsonElement parameterValue)
                        parameter.Value = JsonSerializer.Deserialize(parameterValue.GetRawText(), Type.GetType(parameter.ElementType));
                }

                parameters = new ReadOnlyDictionary<string, IConfigurationElement>(baseConfiguration.Parameters.ToDictionary(k => k.Name, v => v as IConfigurationElement));
            }
        }
    }
}
