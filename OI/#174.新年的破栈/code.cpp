#include<iostream>
#include<vector>
#include<list> 
#include<algorithm>

using namespace std;

//typedef vector<int>::iterator viter;

int main()
{
	int loop_num;
	
	for(cin >> loop_num; loop_num > 0; loop_num--)
	{
        // init
        int cnt; vector<int> p;
        
		cin >> cnt;
		p.reserve(cnt);
        for(int i = cnt; i > 0; i--)
        {
        	int ipt;
            cin >> ipt;
            p.push_back(ipt);
        }
        
        // smallest from left
        vector<int> smlt;
        
        smlt.resize(cnt);
        int mi = smlt[cnt - 1] = p[cnt - 1];
        for(int i = cnt - 2; i >= 0; i--)
        {
            smlt[i] = mi = min(p[i], mi);
        }
        
        list<int> stk; vector<int> rslt;
        
        int pbgn = 0, pend = cnt;
        rslt.reserve(cnt);
		#define PUSH {stk.push_front(p[pbgn]); pbgn++;}
        #define POP {int t = stk.front(); stk.pop_front(); rslt.push_back(t);}
        #define POP_BACK {int t = stk.back(); stk.pop_back(); rslt.push_back(t);}
        #define FRONT (stk.front())
        #define BACK (stk.back())

        while(true)
        {
        	if(!stk.empty())
        	{
        		if(FRONT <= BACK)
        		{
        			if(pbgn >= pend || FRONT <= smlt[pbgn])
					{
						POP;
						continue; 
					}
        		}
				else
				{
					if(pbgn >= pend || BACK <= smlt[pbgn])
					{
						POP_BACK;
						continue; 
					}
				}
        	}
        	if(pbgn < pend)
        	{
        		PUSH;
        	}
        	else break;
        }
            	        	
        // output
        for(int i = 0; i < cnt; i++)
        {
        	cout << rslt[i] << " ";
        }
        cout << "\n";
	}
	
}

