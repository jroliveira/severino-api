namespace Severino.Domain.Shared
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text.RegularExpressions;

    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Extensions;
    using Severino.Infrastructure.Monad;

    using static System.Text.RegularExpressions.Regex;
    using static System.Text.RegularExpressions.RegexOptions;
    using static System.TimeSpan;

    using static Severino.Infrastructure.Security.Md5HashAlgorithm;

    [Serializable]
    public sealed class Email : ValueObject<Email, string>, ISerializable
    {
        private const string NormalizePattern = @"(@)(.+)$";
        private const string ValidationPattern = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        private Email(string value)
            : base(value.ToLowerCase())
        {
        }

        public static Try<Email> NewEmail(Option<string> value) => value.Select<string, Email>(email =>
        {
            if (IsValid(Normalize(email)))
            {
                return new Email(email);
            }

            return new InvalidObjectException("Invalid email.");
        });

        public void GetObjectData(SerializationInfo info, StreamingContext context) => info.AddValue(nameof(Email).ToLower(), ComputeHash(this));

        private static string Normalize(string @this)
        {
            return Replace(@this, NormalizePattern, DomainMapper, RegexOptions.None, FromMilliseconds(200));

            static string DomainMapper(Match match)
            {
                var idn = new IdnMapping();
                var domainName = idn.GetAscii(match.Groups[2].Value);

                return $"{match.Groups[1].Value}{domainName}";
            }
        }

        private static bool IsValid(string @this) => IsMatch(@this, ValidationPattern, IgnoreCase, FromMilliseconds(250));
    }
}
