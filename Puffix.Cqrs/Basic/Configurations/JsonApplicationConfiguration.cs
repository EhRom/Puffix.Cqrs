using Puffix.Cqrs.Configurations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Puffix.Cqrs.Basic.Configurations;

/// <summary>
/// Configuration container (JSON file).
/// </summary>
public class JsonApplicationConfiguration : ApplicationConfiguration
{
    /// <summary>
    /// Ensure configuration is loaded.
    /// </summary>
    public override void EnsureIsLoaded()
    {
        parameters ??= LoadParameters();
    }

    private IReadOnlyDictionary<string, IConfigurationElement> LoadParameters()
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

        if (baseConfiguration == null)
            throw new ArgumentNullException($"The configuration content does not match the required format.");

        foreach (ConfigurationElement parameter in baseConfiguration.Parameters)
        {
            if (parameter != null && parameter?.Value is JsonElement parameterValue)
            {
                Type returnType = Type.GetType(parameter.ElementType);
                if (returnType == null)
                    throw new ArgumentNullException($"The type {parameter.ElementType} type does not exists.");

                parameter.Value = JsonSerializer.Deserialize(parameterValue.GetRawText(), returnType);
            }
        }

        return new ReadOnlyDictionary<string, IConfigurationElement>(baseConfiguration.Parameters.ToDictionary(k => k.Name, v => v as IConfigurationElement));
    }
}
