 public static bool isVowel(char c)
   {
       char lowerChar = char.ToLower(c);
       return lowerChar == 'a' || lowerChar == 'e' || lowerChar == 'i' || lowerChar == 'o' || lowerChar == 'u';
   }
   public static int MaxVowels(string s,int k)
   {
       int vowelCount = 0;

      
       for (int i = 0; i < k; i++)
       {
           
           if (isVowel(s[i]))
           {
               vowelCount++;
           }
       }

       int maxVowels = vowelCount;

       for (int end = k; end < s.Length; end++)
       {
          
           if (isVowel(s[end - k]))
           {
               vowelCount--;
           }

           
           if (isVowel(s[end]))
           {
               vowelCount++;
           }

           
           maxVowels = Math.Max(maxVowels, vowelCount);
       }

       return maxVowels;
   }
