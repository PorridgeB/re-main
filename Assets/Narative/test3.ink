VAR priority = 3

->main
==main==
#BLUEPRINT: blueprintfound
~priority = 1
I have a favour to ask you. I need you to get me a blueprint from the science quadrant.
When you find it, come back to me. #RESUME: nextvisit
->DONE

=nextvisit
No luck finding a blueprint? thats a shame. 
{nextvisit < 2:
    ~priority = 0
You know I might be able make some improvements to your chasis if with one of those blueprints. 
I'm sure some extra firepower must sound entising #RESUME: nextvisit
    
    -else:
    Don't forget me when you do #RESUME: nextvisit
}
->DONE

==blueprintfound==
Wow, I never thought I would see a real weapontech blueprint. now we can get started on the real fun. Give me some time to look this over and i'll 
->DONE