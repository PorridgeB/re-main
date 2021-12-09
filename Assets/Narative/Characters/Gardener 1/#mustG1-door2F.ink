#mustG1:door2F
VAR door = false

-> Start

==Start
Have you changed your mind about the Ghouls? I want you to get the module, but only if you can leave them unharmed.

* [next] -> next1

==next1== 
Are you willing to do that?

*[yes] -> yes
*[no] -> no
*[meaning unclear] -> unclear
*[I am a security Droid, here to assist you] -> assist

==yes
Excellent. I’ll open the door. 
~ door = true
-> DONE

==no
Then I can’t open the door for you. 
-> DONE

==unclear
Keep thinking, and come back when you’ve made a decision. 
-> DONE

==assist
I don’t believe that your mission is to assist us at any cost. I want the Ghouls to be unharmed. Can you do that?

*[yes] -> yes
*[no] -> no
*[meaning unclear] -> unclear
*[I am a security Droid, here to assist you] -> assist2

==assist2
Come back once you're more clear about your purpose. 
    -> END
