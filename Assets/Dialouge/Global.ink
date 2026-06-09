VAR name = ""
VAR item_wood = 0
VAR item_figure = 0
VAR item_candle = 0
VAR q_wood_quest = 0 // 0: NotStarted, 1: InProgress, 2: Completed
VAR q_reliquary_quest = 0
VAR q_candle_quest = 0

=== State_0 ===
~ name = "Char"
Huh?! What is going on? I can hear bells in the distance.
The alley is suposed to be a dead end though.
That is really weird, I should go check to see what's going on.
-> DONE

=== State_0_1 ===
That's the bridge that leads out of the forrest. 
However I should find out, where the bells come from before leaving.
->DONE

=== State_1 ===
Wow, there's palace grounds here? I've never heard about them.
Oh, and there's someone standing by the shrine.
I wonder, what they are doing here.
-> DONE

=== State_1_1 ===
I haven't learned enough about this place to be able to leave just yet.
-> DONE

=== State_2 ===
~ name = "Char"
Hey, sorry to bother you, but may I ask what you are doing here?
~ name = "NPC"
I could ask you the same question. 
I never expected to meet someone in a place like this.
~ name = "Char"
I was just camping close by and heard bells.
Did you hear them as well? Is that why you are here?
~ name = "NPC"
Bells? Oh yeah, of course. I followed the sound of them.
Then I stumbled across this shrine, it's totally destroyed.
That's a shame, don't you think?
I am going to try and fix it, it will take forever though.
I just hope to get it done before dawn.
~ name = "Char"
Oh, you're right, the shrine is collapsed. 
How could that have happened?
Being out here alone in the dark sounds dangerous.
Maybe I can help? It'll be fixed faster.
~ name = "NPC"
You would really do that? That would be so amazing!
Could you start by collecting some wood and bringing it here?
~ name = "Char"
Yes, for sure, I'll be right back # QUEST:wood_quest:START
~ q_wood_quest = 1
-> DONE

=== State_2_Interact ===
{ 
    - q_candle_quest == 2:
        -> State_6
    - q_candle_quest == 1 && item_candle >= 2:
        -> State_5
    - q_candle_quest == 1:
        -> State_4_1
    - q_reliquary_quest == 1 && item_figure >= 1:
        -> State_4
    - q_reliquary_quest == 1:
        {
            - item_figure >= 1:
                -> State_3_2
            - else:
                -> State_3_1
        }
    - q_wood_quest == 2:
        {
            - item_figure >= 1:
                -> State_3_2
            - else:
                -> State_3_1
        }
    - q_wood_quest == 1 && item_wood >= 4:
        -> State_3
    - q_wood_quest == 1:
        -> State_2_1
    - else:
        -> State_2
}

=== State_2_1 ===
~ name = "NPC"
It doesn't look like you gathered enough wood yet.
-> END

=== State_3 ===
~ name = "Char"
Is this enough?
~ name = "NPC"
Absolutely, that helps a lot.
# QUEST:wood_quest:COMPLETE
~ q_wood_quest = 2
The reliquary is missing, it should be in a big house near by.
Would you bring that here as well?
~ name = "Char"
You're right, the spot where it should be is empty.
How do you know where it is?
~ name = "NPC"
It's just a hunch.
~ name = "Char"
Fair enough, I'll be right back. # QUEST:reliquary_quest:START
~ q_reliquary_quest = 1
-> END

=== State_3_1 ===
~ name = "NPC"
The reliquary spot is still empty...
-> END

=== State_3_2 ===
~ name = "Char"
It was actually in a big house. How did she know?
This is starting to feel a little weird.
-> END

=== State_4 ===
~ name = "Char"
I found it in a big house east from here.
How did you know it was going to be there?
~ name = "NPC"
I told you, it was just a hunch.
# QUEST:reliquary_quest:COMPLETE
~ q_reliquary_quest = 2
Besides we're almost done. The only thing missing are two candles.
I'm sure you will find some in the houses north.
~ name = "Char"
Is that another one of your hunches?
~ name = "NPC"
Yes, don't overthink it. Just bring them here.
~ name = "Char"
It's definitely weird now... 
Whatever, let's find these candles and get of here. # QUEST:candle_quest:START
~ q_candle_quest = 1
-> END

=== State_4_1 ===
~ name = "NPC"
I don't see any candles yet.
-> DONE

=== State_5 ===
~ name = "NPC"
Finally! That took you a while.
~ name = "Char"
You could really show more graditute.
I'm helping you out after all.
~ name = "NPC"
# QUEST:candle_quest:COMPLETE
~ q_candle_quest = 2
Greatful towards a lowly human like you?
I am a powerful demon spirit, we do not compare.
It was your kind that destroyed my shrine, binding me to this spot.
Your soul shall be mine as a small repayment from you mere humans.
~ name = "Char"
What? What are you talking about? That is so creepy! 
I'm leaving this place right this moment.
~ name = "NPC"
There's no escape, I entraped you in this area right when you entered.
~ name = "Char"
We'll see about that!
-> DONE

=== State_6 ===
~ name = "NPC"
Have you come to your senses and realized you can't get away from me?
~ name = "Char"
Please just let me go. I have done nothing to harm you.
~ name = "NPC"
Why should I do that? Your soul is mine now.
-> END

=== State_7 ===
~ name = "Char"
There is no need for this right now
-> END
