# Prime Rollover - Text Based Harness

The purpose of this harness, is to illustrate the patterns and sequences that can be derived by use of Prime Rollover. This technique can be used to emulate randomness, rapidly and efficiently.


## From In App Documentation

Prime Rollover is a generic process for iterating a list of items, knowing you have used every item once-and-only-once, and either quitting, going again in the same pattern, or selecting a new increment, when returning to the starting-point. In-short, Prime Rollover is a pseudo-randomizer, that does not rely on re-draws or eliminating duplicates, while enforcing even-usage of elements, and is extremely performance efficient.

For verbal illustration, let’s use the old fence-post analogy. You have seven fence posts in a row, and need to connect them. For this, you use six sections of fence. To understand what is happening in Prime Rollover, you need to first arrange these fence posts into a circle. How many sections of fence do you need? You now need seven sections of fence to complete the circle. When we use Prime Rollover, we treat the values as though our list is a circle.

This gets better. We are well acquainted with the indivisibility of primes. In a normal circumstance, we either say that it cannot be done, or results in a fraction. Prime Rollover is an exception. Every prime number is evenly divisible everytime, from a Prime Rollover perspective. If anything, this makes primes just as unique in Prime Rollover, as they are in the ordinary perspective of numbers (non-prime numbers quit before using every element, they fail). Everything I just said, is something of a misnomer.

Moving back to our example of a fence, arranged in a circle, and getting back to the nuts and bolts of what Prime Rollover is, think of connecting every fence-post, to two-different fence sections, without duplicating lines. The shape of a circle stops mattering. What you get, is either a circle-perimeter (increment = 1, as in our first case), or star shapes if incrementing by any number greater-than-one but less than P-1 (P-1 is also a circle, travelled backwards). What you see, is the visual attempt to divide a prime, and collecting the remainder until full.

To make this more clear, when walking the perimeter, we have effectively divided the prime number by one, touched every fence-post in order, and on the P-section of fence, returned to where we were. We divided P by 1, and got P sections of fence, two for every post. Just as important, we did this in one-revolution of the circle.

Now, clear everything but the posts, and increment by 2, touching every-other post in order, until you reach the start. You have effectively divided P by 2, taking 2 revolutions around the circle to perform, for a total of P * 2 posts, or 14-posts.

You can see now, that we are not actually dividing a prime. We are using the optical illustration of Prime Rollover, to see that out of 7-posts, you can only touch 3.5 of them per revolution, when dividing by two - but inherently, you touch every post, without repeat, before returning to where you began. This is the quality that makes Prime Rollover an ideal pseudo-randomizer. The remainder collects, until whole, on the N iteration. We simultaneously try to divide prime by some smaller number N, and multiply prime by that same number N, to arrive at everything being used after N iterations through list.

Put plainly, (P * N) / N always divides evenly and usefully, and always equals P stops along the way when comprised as a circular-list.

<hr>

Uses for Prime Rollover vary. The most obvious uses, are making a large number of assignments, from a limited pool, where even-usage is important. That said, I have used it in very-small projects, for something as simple as randomizing answers on a multiple-choice form.

The obvious draw back, is the list must contain a prime number of elements. In the example of multiple choice, my lists needed to be 5 or 7 elements long, even if there were fewer answers. The simple workaround, is to have null elements at the end, and discard null when passing by.
