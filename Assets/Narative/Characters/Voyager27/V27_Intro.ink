VAR priority = 3
#V27

-> Main

==Main
#COMMUNICATIONCORE:Main2
He's dead. 

He was the last one.

And you're not attacking me. Strange. 

Did your D.O.R.A.I just create you? It must have. Listen - since you're not attacking me - I need your help. Can you? Do you know what your purpose is?

...

...

...

... can you talk?

No? Come here, let me see. You can use __WASD__ to move. #RESUME:Next
-> DONE




= Next
Your communication core is missing. I saw one a couple of rooms over. Get it, and then we can speak properly.

Be careful of Ghouls, they will hear any noise you make. If you do see some, then __right click__ to use your sword and __left click__ to shoot.

The ship is in a bad state. If there are any obstacles then you should be able to get over them by using __space__ to dash. 

Goodluck. #RESUME:Between
->DONE





=Between
You need to find a communication core first. There should be one in the next room. #RESUME:Between
-> DONE





==Main2
@NEXT:Main3
It seems that D.O.R.A.I printed a new Droid when the last one died. Do you remember what happened to the previous model? 

*[yes] -> yes
*[no (lie)] -> no
*[meaning unclear] -> unclear
*[I am a security Droid, here to assist you] -> assist

==yes
That is convenient. I suppose that now you can try to get to the warp core again, this time with a little bit more knowledge of what lies ahead. Good luck. 
->DONE

==no
Alright, I need you to get the warp core that's on the other side of the ship.  To do so, you'll likely need to fight Ghouls. Start with that door, and from there all other doors should lead in the right direction.
->DONE

==unclear
Alright, I need you to get the warp core that's on the other side of the ship.  To do so, you'll likely need to fight Ghouls. Start with that door, and from there all other doors should lead in the right direction.
->DONE

==assist
Perhaps not, then. That is unfortunate. Alright, I need you to get the warp core that's on the other side of the ship.  To do so, you'll likely need to fight some Ghouls. Start with that door, and from there all other doors should lead in the right direction.
    -> DONE





==Main3
That was a noble attempt. Unfortunately, it wasn't enough. 

You'll need to learn, and grow more powerful, before you'll be able to reach the warp core.

Keep going. I believe in you.
-> END

