#FIRST
VAR hasScrap = 0
VAR hasModule = 0

->main

==main==

So you're the bot that I've been hearing about? I thought you would be taller.
Whatever, now that youre here, if you find any scrap out there let me know, will you? #RESUME: scrap
{hasScrap:
    ->scrap
  - else:
    ->DONE
}
=scrap
{hasScrap:
    Scrap! I can see it! GIMME GIMME GIMME
    -> crafting
  - else:
    Come back to me when you find some scrap
}
->DONE

=crafting
now that i have some scrap, i can craft you this epic item #RESUME: scrap
~ hasScrap = 0
->DONE