### TASK
https://docs.google.com/document/d/1HY7Dl-5itYD3C_gkueBvvBFpT4CecGPiR30BsARlTpQ/edit

Linear congruential generator. {Mode} in link is “Lcg”. Numbers are generated like this:
```c#
public int Next()
{
	_last = (a * _last + c) % m; // m is 2^32
	return (int) _last;
}
```
The first one who writes “a” and “c” values to group chat will get +1 scores.  
Lesson to learn: just never use LCG for anything.

### OUTPUT
a = 1664525  
c = 1013904223  
https://docs.google.com/document/d/1jhf3P6Iob5fxN4EkM9illeYgAnzwmaCJ2SbSkzpftH4/edit  