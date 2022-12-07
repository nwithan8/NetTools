using CrypticWizard.RandomWordGenerator;

namespace NetTools.Crypto;

public static class Passwords
{
    public static double GenerateRandomPasscode(int length = 8)
    {
        return new Random().Next(0, (int)System.Math.Pow(10, length));
    }

    public static string GenerateRandomPassphrase(int minLength = 16, int maxLength = 64)
    {
        var generator = new WordGenerator();

        // generate a (adjective-)noun phrase
        var phrase = generator.GetWord(WordGenerator.PartOfSpeech.noun);

        var loopCount = 0;
        while (phrase.Length < minLength)
        {
            if (loopCount > 20)
                // prevent infinite loop
                // we've tried and failed 20 times to generate a phrase that falls between the min and max length
                throw new Exception("Unable to generate a passphrase that falls between the min and max length");

            var adjective = generator.GetWord(WordGenerator.PartOfSpeech.adj);
            if (adjective.Length + phrase.Length + 1 < maxLength) // adding the word and a hyphen wouldn't exceed the max length
                phrase = $"{adjective}-{phrase}";

            loopCount++;
        }

        return phrase;
    }

    public static string GenerateRandomPassword(int length = 16, bool allowSpecialCharacters = true)
    {
        // ReSharper disable once StringLiteralTypo
        var allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        if (allowSpecialCharacters) allowedCharacters += "!@#$%^&*()_+-=[]{}|;':,./<>?";

        var chars = allowedCharacters.ToCharArray();

        var password = string.Empty;
        var random = new Random();
        for (var i = 0; i < length; i++)
        {
            var x = random.Next(1, chars.Length);
            password += chars.GetValue(x);
        }

        return password;
    }
}
