using System;
using System.Collections.Generic;
namespace CyberSecurityChatbot.Models
{
    public class ResponseManager
    {
        private Dictionary<string, string> keywordResponses;
        private List<string> phishingTips;
        private Random random;
        private string currentTopic = ""; private ChatMemory memory;
        private SentimentDetector detector;
        public delegate string ResponseDelegate(string input);
        public ResponseManager()
        {
            random = new Random();
            memory = new ChatMemory();
            detector = new SentimentDetector();
            keywordResponses = new Dictionary<string, string>()
{
 {
"password",
"Use strong passwords with numbers, symbols, and uppercase letters."
 },
 {
 "privacy",
"Review your privacy settings regularly and avoid oversharing online."
 },
 {
 "scam",
"Avoid clicking suspicious links and never share personal information with strangers."
},
{
"phishing",
"Phishing scams often pretend to be trusted companies."
 }
 };
            phishingTips = new List<string>()
 {
 "Be careful of urgent emails requesting personal information.",
"Always verify the sender email address before clicking links.",
"Do not download attachments from unknown senders.",
"Scammers often create fake login pages to steal passwords."
 };
        }
        public string GetResponse(string input)
        {
            input = input.ToLower();
            string sentiment = detector.DetectSentiment(input);
            if (sentiment == "worried")
            {
                return "It is understandable to feel worried about cybersecurity threats. Here is a tip: Never share OTP codes with anyone.";
            }
            if (sentiment == "frustrated")
            {
                return "Cybersecurity can feel overwhelming sometimes, but learning small safety habits helps a lot.";
            }
            if (sentiment == "curious")
            {
                return "Curiosity is great! Learning about cybersecurity helps protect your information online.";
            }
            // MEMORY
            if (input.Contains("my name is"))
            {
                string[] words = input.Split(' ');
                memory.UserName = words[words.Length - 1];
                return $"Nice to meet you, {memory.UserName}!";
            }
            if (input.Contains("i like privacy"))
            {
                memory.FavouriteTopic = "privacy";
                currentTopic = "privacy";
                return "Great! I'll remember that you're interested in privacy.";
            }
            // FOLLOW UP RESPONSES
            if (input.Contains("tell me more")
            || input.Contains("another tip")
            || input.Contains("explain more"))
            {
                if (currentTopic == "password")
                {
                    return "Enable two-factor authentication to make your accounts more secure.";
                }
                if (currentTopic == "privacy")
                {
                    return "Avoid posting personal details like your address or phone number publicly.";
                }
                if (currentTopic == "phishing")
                {
                    return phishingTips[random.Next(phishingTips.Count)];
                }
            }
            // RANDOM PHISHING TIPS 
            if (input.Contains("phishing"))
            {
                currentTopic = "phishing";
                return phishingTips[random.Next(phishingTips.Count)];
            }
            // KEYWORD RECOGNITION 
            foreach (var keyword in keywordResponses.Keys)
            {
                if (input.Contains(keyword))
                {
                    currentTopic = keyword;
                    string response = keywordResponses[keyword];
                    if (memory.FavouriteTopic == keyword)
                    {
                        response += $" Since you're interested in {keyword}, make sure to stay updated on online safety practices.";
                    }
                    return response;
                }
            }
            // USER NAME RECALL 
            if (input.Contains("who am i"))
            {
                if (!string.IsNullOrEmpty(memory.UserName))
                {
                    return $"You told me your name is {memory.UserName}.";
                }
                return "I don't know your name yet.";
            }
            // DEFAULT RESPONSE
            return "I'm not sure I understand. Can you try rephrasing?";
        }
    }
}
