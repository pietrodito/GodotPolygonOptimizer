using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class UpFnRect : CollisionPolygon2D
{
    [Export]
    private Vector2 _size = new Vector2(700, 500);

    [Export]
    private int _precision = 3000;

    private double[] _x_values;
    private double[] _y_values;

    public override void _Ready()
    {
        _x_values = new double[_precision + 1];
        _y_values = new double[_precision + 1];
        for (int i = 0; i <= _precision; i++)
        {
            var _x_01_values = new double[_precision + 1];
            _x_01_values[i] = (double) i / (double) _precision;
            _x_values[i] = _x_01_values[i] * _size.x;
            _y_values[i] = Math.Max(SideUpFn(_x_01_values[i]) * _size.y, 0);
        }
        Vector2 upLeftPoint = new Vector2 (Convert.ToInt32(_x_values[0]),
                                           Convert.ToInt32(_y_values[0]));
        Vector2 upRightPoint = new Vector2(Convert.ToInt32(_x_values[_precision]),
                                           Convert.ToInt32(_y_values[_precision]));
        List<double> x_lst_dbl = _x_values.ToList();
        List<double> y_lst_dbl = _y_values.ToList();
        List<int> x_lst_int = x_lst_dbl.
                                Select(x => Convert.ToInt32(x)).ToList();
        List<int> y_lst_int = y_lst_dbl.
                                Select(y => Convert.ToInt32(y)).ToList();
        List<Vector2> points = x_lst_int.Zip(
                    y_lst_int, (x, y) => new Vector2(x, y)).Distinct().ToList();

        points = points.FindAll(p => ! TouchSides(p));
        
        points.Add(upRightPoint);
        points.Add(new Vector2(_size.x, _size.y));
        points.Add(new Vector2(0, _size.y));
        points.Add(upLeftPoint);

        Polygon = points.ToArray();
    }

    private bool TouchSides(Vector2 point)
    {
        return point.x == 0 || point.y == _size.y || point.x == _size.x;
    }

    private double SideUpFn(double x)
    {
        //return Math.Sin(x * Math.PI) * .9f ;
        //return Math.Sqrt(x);
        return x * x;
    }
}