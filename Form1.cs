using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// 어샘블리(프로젝트) 전체에서 접근 가능한 영역
//int global_number; // C# 7.3에서는 안됨

namespace WindowsFormsApp1_250102
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // ref와 out 메서드 예제 사용
            int[] array1 = new int[5];
            FillArray_Ref(ref array1);
            foreach (int num in array1)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();

            FillArray_Out(10, out int[] array2);
            foreach (int num in array2)
            {
                Console.Write(num + " ");
            }
        }

        // ref를 사용하는 메서드
        private void FillArray_Ref(ref int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i + 1;
            }
        }

        // out을 사용하는 메서드
        private void FillArray_Out(int size, out int[] array)
        {
            array = new int[size];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i + 1;
            }
        }
    }
}

