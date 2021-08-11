using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using Microsoft.International.Converters.PinYinConverter;

namespace UI.test
{
    public partial class PinyinTest : Form
    {
        public PinyinTest()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            PinyinHelper.GetPinyin(txtPy.Text);

           /* //清空文本字符串
            lblPy.Text = "";
            //获取用户输入的字符串
            string txt=txtPy.Text;
            //逐个遍历字符，获取对应的拼音
            foreach (char c in txt)
            {
                //判断指定的字符，是否是汉字
                if (ChineseChar.IsValidChar(c)){
                    //是汉字则构造字符对象
                    ChineseChar cc = new ChineseChar(c);
                    //cc.Pinyins返回的是一个集合，因为存在多音字的情况，所以要遍历该集合
                    //foreach (var c1 in cc.Pinyins)
                    //{
                    //    lblPy.Text += c1 + " ";
                    //}
                    //获取拼音首字母
                    lblPy.Text += cc.Pinyins[0][0];
                }
                else
                {
                    //不是汉字直接拼接
                    lblPy.Text += c;                     
                }             
            }*/
        }

        private void PinyinTest_Load(object sender, EventArgs e)
        {

        }
    }
}
