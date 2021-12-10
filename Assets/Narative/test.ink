#FIRST
VAR priority = 2
VAR hasScrap = 0

->main

==main==
~priority = 0
So you're the bot that I've been hearing about? I thought you would be taller.
Whatever, now that youre here, if you find any scrap out there let me know, will you? #RESUME: scrap
{hasScrap:
    ->crafting
  - else:
    ->DONE
}
=scrap
#SCRAP: crafting
Come back to me when you find some scrap
->DONE

==crafting==
now that i have some scrap, i can craft you this epic item #RESUME: scrap
~ hasScrap = 0
->DONE