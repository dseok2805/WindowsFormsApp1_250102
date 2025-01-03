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

namespace WindowsFormsApp1_250102
{
    public partial class Form1 : Form
    {
        // 딕셔너리 초기화
        Dictionary<string, string> idPasswordDict = new Dictionary<string, string>();
        Dictionary<string, string> idPhoneDict = new Dictionary<string, string>();
        public Form1()
        {
            InitializeComponent();
            // 버튼 추가
            Button openFileButton = new Button();
            openFileButton.Text = "파일 열기";
            openFileButton.Dock = DockStyle.Top;
            openFileButton.Click += OpenFileButton_Click;
            Controls.Add(openFileButton);
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "텍스트 파일 (*.txt)|*.txt|모든 파일 (*.*)|*.*";
                openFileDialog.Title = "파일 열기";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        using (StreamReader reader = new StreamReader(filePath))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                // 한 줄을 읽고 ,로 분리
                                string[] parts = line.Split(',');

                                if (parts.Length >= 2) // 아이디와 비밀번호는 반드시 있어야 함
                                {
                                    string id = parts[0].Trim();
                                    string password = parts[1].Trim();
                                    string phone = parts.Length > 2 ? parts[2].Trim() : null;

                                    // 딕셔너리에 추가
                                    idPasswordDict[id] = password;
                                    idPhoneDict[id] = phone;
                                }
                            }
                        }

                        // 결과 확인용 (메시지 박스로 출력)
                        string result = "ID-Password Dictionary:\n";
                        foreach (var pair in idPasswordDict)
                        {
                            result += $"{pair.Key}: {pair.Value}\n";
                        }

                        result += "\nID-Phone Dictionary:\n";
                        foreach (var pair in idPhoneDict)
                        {
                            result += $"{pair.Key}: {pair.Value ?? "null"}\n";
                        }

                        MessageBox.Show(result, "딕셔너리 저장 결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"파일을 처리하는 중 오류가 발생했습니다.\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button_LOGIN_Click(object sender, EventArgs e)
        {
            string inputId = textBox_ID.Text.Trim();
            string inputPassword = textBox_PW.Text.Trim();

            if (idPasswordDict.ContainsKey(inputId))
            {
                if (idPasswordDict[inputId] == inputPassword)
                {
                    // 로그인 성공
                    string phone = idPhoneDict[inputId] ?? "등록되지 않음";
                    MessageBox.Show($"로그인 성공!\nID: {inputId}\n전화번호: {phone}", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // 비밀번호 불일치
                    MessageBox.Show("로그인 실패: 비밀번호가 일치하지 않습니다.", "실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                bool passwordMatches = idPasswordDict.Values.Contains(inputPassword);
                if (passwordMatches)
                {
                    // 아이디 불일치
                    MessageBox.Show("로그인 실패: 아이디가 일치하지 않습니다.", "실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // 아이디와 비밀번호 모두 불일치
                    MessageBox.Show("로그인 실패: 아이디와 비밀번호가 모두 일치하지 않습니다.", "실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}