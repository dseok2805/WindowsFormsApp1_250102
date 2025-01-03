using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        }

        /// <summary>
        /// 파일 경로와 배열을 입력받아 파일 내용을 배열에 저장하는 메소드
        /// </summary>
        /// <param name="path">파일 경로</param>
        /// <param name="lines">파일의 각 줄을 저장할 배열</param>
        private void ReadFileLines(string path, out string[] lines)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("지정된 파일 경로가 존재하지 않습니다.");
            }

            // 파일 읽기 및 줄 단위로 배열에 저장
            lines = File.ReadAllLines(path);
        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            // 파일 경로 및 내용을 저장할 배열 선언
            string filePath = txtFilePath.Text.Trim('"'); // 큰따옴표 제거
            string[] fileLines = null; // 초기화

            try
            {
                // 파일 경로 및 배열을 전달하고 내용을 저장
                ReadFileLines(filePath, out fileLines);

                // ListBox에 파일 내용 출력
                lstFileContent.Items.Clear();
                foreach (string line in fileLines)
                {
                    lstFileContent.Items.Add(line);
                }
                MessageBox.Show("파일 읽기가 완료되었습니다.");
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("파일을 찾을 수 없습니다: " + ex.Message);
            }
            catch (IOException ex)
            {
                MessageBox.Show("입출력 오류: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("알 수 없는 오류: " + ex.Message);
            }
            finally
            {
                // 작업 완료 메시지 표시 또는 리소스 해제
                MessageBox.Show("파일 읽기 작업이 종료되었습니다.");
                // 추가적으로, 필요하면 ListBox 초기화 또는 다른 정리 작업 수행
                if (fileLines == null)
                {
                    lstFileContent.Items.Clear(); // 실패 시 ListBox 초기화
                }
            }
        }
    }
}


