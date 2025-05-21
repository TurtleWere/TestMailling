using Mailling.Core;
namespace Mailling.Application;

public class MailService
{
    private static readonly Dictionary<string, List<string>> DomainRules = new Dictionary<string, List<string>>
    {
        { "tbank.ru", new List<string> { "t.tbankovich@tbank.ru", "v.veronickovna@tbank.ru" } },
        { "alfa.com", new List<string> { "v.vladislavovich@alfa.com" } },
        { "vtb.ru", new List<string> { "a.aleksandrov@vtb.ru" } }
    };

    private static readonly Dictionary<string, List<string>> DomainExceptions = new Dictionary<string, List<string>>
    {
        { "tbank.ru", new List<string> { "i.ivanov@tbank.ru" } },
        { "alfa.com", new List<string> { "s.sergeev@alfa.com", "a.andreev@alfa.com" } },
        { "vtb.ru", new List<string>() }
    };

    public Mail CreateResponse(Mail request)
    {
        var response = new Mail
        {
            From = request.From,
            To = request.To,
            Copy = request.Copy,
            BlindCopy = request.BlindCopy,
            Title = request.Title,
            Body = request.Body,
        };

        try
        {
            var toAddresses = ParseAddresses(request.To);
            var copyAddresses = ParseAddresses(request.Copy);
            var allAddresses = toAddresses.Concat(copyAddresses).ToList();
            var domains = GetDomains(allAddresses);

            if (domains.Any(d => DomainRules.ContainsKey(d)))
                if (DomainExceptions.Values.Any(list => list.Intersect(allAddresses).Any()))
                {
                    response.To = DeleteAddresses(toAddresses, allAddresses);
                    response.Copy = DeleteAddresses(copyAddresses, allAddresses);
                }
                else
                {
                    response.Copy = AddressSubstitution(copyAddresses, domains);
                }

            return response;

        }
        catch (Exception ex)
        {
            response.Message = $"{ex.Message}";
            return response;
        }

    }

    private List<string> ParseAddresses(string addresses)
    {
        if (string.IsNullOrWhiteSpace(addresses))
            return new List<string>();
        char[] delimiters = { ';', ' ' };
        var result = addresses.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
        return result;
    }
    private string DeleteAddresses(List<string> addresses, List<string> allAddresses)
    {
        var duplcates = DomainRules.Values.SelectMany(list => list).Intersect(allAddresses).ToList();

        addresses.RemoveAll(item => duplcates.Contains(item));

        return string.Join("; ", addresses);
    }

    private string AddressSubstitution(List<string> addresses, List<string> domains)
    {
        var newDomains = domains.Where(d => DomainRules.ContainsKey(d)).ToList();
        foreach (var domain in newDomains)
        {
            foreach (var address in DomainRules[domain])
            {
                if (!addresses.Contains(address))
                {
                    addresses.Add(address);
                }
            }
        }
        return string.Join("; ", addresses);

    }
    private List<string> GetDomains(List<string> allAddresses)
    {
        var domains = allAddresses.Select(a => a.Split('@').Last()).ToList();

        return domains;
    }

}