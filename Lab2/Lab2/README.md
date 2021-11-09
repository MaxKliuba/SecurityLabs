## TASK
https://docs.google.com/document/d/121efoh98-uQQdgpz1fc_Zu27SiNLzib1o2Ah_sr5f1Y/edit?usp=sharing

This is normal text ciphered with a strong algorithm (Salsa20), but every line is ciphered with the same key and no nonce is being used. Name the author of these lines. The first team to do this in our chat will receive +2 score.  
If you are not the first - this task is graded based on your code or written description of your solution.

## INPUT
280dc9e47f3352c307f6d894ee8d534313429a79c1d8a6021f8a8eabca919cfb685a0d468973625e757490daa981ea6b  
3a0a9cab782b4f8603eac28aadde1151005fd46a859df21d12c38eaa858596bf2548000e883d72117466c5c3a580f66b  
3a0adee4783a538403b9c29eaac958550242d3778ed9a61918959bf4ca849afa68450f5edc6e311a7f7ed1d7ec  
3a0adee461354e8c1cfcc39bef8d5e40525fdc6bc0dee359578290bcca849afa685a1e5c897362  
3a0adab0282b5c9719fcc38caac054541b449a62cf9df21d509690af858286f731091a4890786252  
390adeaa283358c318f0c08befc157061f59dd65dd9dee1c04c38fad839586ea3b0903489078  
390bcfac283a1d8111ebc8d8e8c2554d1b5e852dfed5e955008c8bb48ed094fe3a4d0b45883d731b7b609c  
3a0d9ba37a2e539750f8c39caade464313449a78c7d9e3075782deaf8f9180e66845074f9e31  
2c17cfe47c335c9750edc59daac9434313549a62cf9df51a1a868ab0839e95bf294f1a4c893d751b7b66d882  
3a0adee47d35598a03fac28eefdf54011610d962dcd3f2070ecfdebe989f9fbf3f41015a9e3d73116f60de  
200d9bb07a3a4b861cf5c88aaadf54520742d47e859df6000d9992bd99d086f72d09194097713d  
2f0cdfe4653a568603b9d88baadf50521a55c82dcbd8e707579796b79995d2f624451d098c7831167b64d5  
3a0adaaa283d519a50edc2d8e5d9594300439a79c1dcf2550086deb3849f85bf26461a09947b2e  
3a0aceb72838528d03fac49de4ce5406165fce6589d0e71e12c39db79d9180fb3b09014fdb68625e7b7edc82  
2f0cdfe47c33489050edc59daac350521b46df2dc1c8e3551885deaa8f839df33d5d074695  
27119bb76138568f19fcc9d8e58a54545247d379c19df21d12c38eb98695d2fc295a1a09947b310a727dc5c9a898a3  
2f0cdfe46d35498602e9df91f9c842061d569a6adbd8e701579397ac82d093f12c09034696787f0a  
390bcfac282f558a03b9df9dedcc43425244d268c0cfa61602918cbd848481bf3c5c1c47db7c660c63  
2f0cdfe464344e8650edc59daac3504b1710d56b89dce5011e8c90f6  

## DESCRIPTION
Перш ніж зламати шифр, потрібно підсумувати усю інформацію, яка нам відома про нього. Отже, ми знаємо, що тут зашифровано англійський текст. Оскільки потрібно дізнатися автора, то це якийсь твір. 
А судячи із довжини і форми рядків, це вірш або якась поема. Також відомо, що текст шифрувався алгоритмом Salsa20 із повторно використаним ключем (кожен рядок шифрується одним ключем).  
Salsa20 відноситься до алгоритмів потокового шифрування. Такі шифри можна зламати використовуючи reused key attack і метод crib dragging. 
Суть даних підходів полягає у використанні комутативної властивості операції XOR, яка лежить у основі роботи потокових алгоритмів шифрування.  
Отже, для дешифрування потрібно виконати наступні кроки:  
1. Здогадатися, яке слово може бути у даних зашифрованих рядках.
2. Зробити XOR двох зашифрованих рядків.
3. Зробити XOR вгаданого слова із кожною позицією результуючого рядка із пункту 2.
4. У місці співпадіння вгаданого слова із зашифрованим рядком появляється фрагмент розшифрованого тексту. 
5. Повторювати пункти 1-4 із доповнюванням, отриманим з фрагмента розшифрованого тексту, поки текст не буде можливим для читання.  

Оскільки у англійських текстах (особливо у літературних творах) дуже часто зустрічається "The", то варто почати саме з цього. Аналізуючи шифротекст можна побачити, що перші 4 символи у декількох рядках співпадають. Припустимо це саме "The " (із пробілом).
Була написана програма, яка робить операцію XOR кожного рядка із іншими, а після цього XOR із вгаданим фрагментом (у даному випадку із "The "). Серед отриманих результатів обирається найкращий варіант. Запустивши цю програму можна побачити, що справді отримано фрагмент розшифрованого тексту, а точніше перші 4 символи кожного рядка.
Серед даних розшифрованих фрагментів можна зустріти "When" на 5 рядку. Очевидно, після цього буде іти пробіл. Отже, запустимо програму знову, але уже із "When " (із пробілом).  
Проводячи аналогічні дії, можна піти по наступному логічному ланцюжку: "The " -> [5] "When " -> [8] "But that " -> [11] "And makes " -> [15] "Is sicklied " -> [0] "For who would ".  
Таким чином було отримано перші 14 символів кожного рядка. Увівши деякі із них у пошукому систему Google, можна отримати декілька релевантних результатів, які писилаються на твір Вільяма Шекспіра "Гамлет", а точніше на фрагмент відомого монологу Гамлета. 
Даний результат цілком співпадає із відомою нам інформацією, що говорить про його правильність. Взявши повністю перший рядок "For who would bear the whips and scorns of time,", який є одним із найдовших, і запустивши програму, можна отримати повністю розшифрований текст.

## OUTPUT
BY WILLIAM SHAKESPEARE  
(from Hamlet, spoken by Hamlet)  

For who would bear the whips and scorns of time,  
Th'oppressor's wrong, the proud man's contumely,  
The pangs of dispriz'd love, the law's delay,  
The insolence of office, and the spurns  
That patient merit of th'unworthy takes,  
When he himself might his quietus make  
With a bare bodkin? Who would fardels bear,  
To grunt and sweat under a weary life,  
But that the dread of something after death,  
The undiscovere'd country, from whose bourn  
No traveller returns, puzzles the will,  
And makes us rather bear those ills we have  
Than fly to others that we know not of?  
Thus conscience doth make cowards of us all,  
And thus the native hue of resolution  
Is sicklied o'er with the pale cast of thought,  
And enterprises of great pith and moment  
With this regard their currents turn awry  
And lose the name of action.  