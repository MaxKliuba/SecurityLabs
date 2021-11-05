## TASK
https://docs.google.com/document/d/1HY7Dl-5itYD3C_gkueBvvBFpT4CecGPiR30BsARlTpQ/edit

MT19937 with a strong seed. {Mode} in link is “BetterMt”. Seed is created with “System.Security.Cryptography.RandomNumberGenerator.Create()”. You need to extract the whole state of every register of MT to break this one. Create a new MT19937 generator, tap it for 624 outputs, untemper each of them to recreate the state of the generator, and splice that state into a new instance of the MT19937 generator. Use it to predict next values.  
Lesson to learn: Weak algorithm turns a strong seed into garbage. The security of a system is made up not by the sum of parts but by the min.

## OUTPUT
https://docs.google.com/document/d/121efoh98-uQQdgpz1fc_Zu27SiNLzib1o2Ah_sr5f1Y/edit?usp=sharing 