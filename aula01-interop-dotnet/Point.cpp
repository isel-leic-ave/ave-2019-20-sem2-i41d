using namespace System;

public ref class Point {
    protected:
        int _x, _y;
    public:
        Point(int x, int y) {
            this->_x = x;
            this->_y = y;
        }

        double getModule() {	 
            return Math::Sqrt((double)_x*_x + _y*_y);
        }
};