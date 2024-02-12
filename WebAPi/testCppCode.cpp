#include<iostream>
#include<cstdlib>
#include<ctime> 

using namespace std;

int main() {

    // Providing a seed value
	srand((unsigned) time(NULL));

	// Get a random number
	int randomNumber = rand();

    // Output the random number
    cout << randomNumber << endl;

    return 0;
}
