#mustG1:door2
VAR door = false

-> Start 

==Start
I'm more impatient for you to succeed than I'd like to admit. This life, this responsibility, is no longer for me. 

* [next] -> next1

==next1== 
Behind the door - where our loved ones are - there is something that I think could help you. It’s a module, I think, something that could upgrade your software. 

* [next] -> next2

==next2== 
I can open the door for you, but you need to promise me that you won’t hurt the Ghouls inside. Sneak, or find ways around them, or run. I'm not sure what it's like on the other side but I hope you'll be able to find a way through without hurting anyone. Do you believe you can do that? 

*[yes] -> yes
*[no] -> no
*[meaning unclear] -> unclear
*[I am a security Droid, here to assist you] -> assist

==yes
Excellent. I'll open the door for you. Enter when you're ready
~ door = true

-> END

==no
Then I’m afraid the module will stay where it is. I’m sorry. No matter how much I want to get out of here I can’t abandon the people I’ve protected for over twenty years.
-> END

==unclear
Think about it; go to the door once you’ve made your decision.
-> END

==assist
I don’t believe that your mission is to assist us at any cost. Go to the door once you’re more clear about your decision. I want the Ghouls to be unharmed.


    -> END
