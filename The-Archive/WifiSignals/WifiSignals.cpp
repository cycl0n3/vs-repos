#include <iostream>
#include <vector>
#include <cmath>
#include <chrono>

using namespace std;

struct Point {
    int x, y, z;
};

void drawCube(vector<Point> points) {
    for (int i = 0; i < 6; i++) {
        for (int j = 0; j < 4; j++) {
            cout << points[i * 4 + j].x << " " << points[i * 4 + j].y << " " << points[i * 4 + j].z << endl;
        }
    }
}

int main() {
    vector<Point> points = {
      {-1, -1, -1}, {1, -1, -1}, {1, 1, -1}, {-1, 1, -1},
      {-1, -1, 1}, {1, -1, 1}, {1, 1, 1}, {-1, 1, 1}
    };

    float angle = 0.0f;
    int x = 0;
    while (x++ < 10) {
        angle += 0.01f;

        for (int i = 0; i < 8; i++) {
            points[i].x = points[i].x * cos(angle) - points[i].z * sin(angle);
            points[i].z = points[i].x * sin(angle) + points[i].z * cos(angle);
        }

        drawCube(points);

        cout << endl;
        cout.flush();

        // Sleep for 10 milliseconds to slow down the rotation.
        // You can increase or decrease this value to change the speed of the rotation.
        std::this_thread::sleep_for(std::chrono::milliseconds(1000));
    }

    return 0;
}
