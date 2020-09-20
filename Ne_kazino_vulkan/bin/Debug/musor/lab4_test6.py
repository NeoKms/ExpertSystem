def print_m(m):
    for i in range(len(m)):
        print(m[i])
        
def del_povtor(now):
    now_no_powt=[]
    ind_for_del=[]
    
    for i in range(len(now)):
        bol=True
        
        for j in range(len(ind_for_del)):
            
            if i==ind_for_del[j]:
                
                bol=False
                
        if bol:
            now_no_powt.append("".join(set(now[i])))
            
    now_2=[]
    for i in range(len(now_no_powt)):
            spl=list(now_no_powt[i])
            spl.sort()
            now_2.append(spl)
            
    now_3=[]
    for i in range(len(now_2)):
        ss=''
        for j in range(len(now_2[i])):
            ss=ss+str(now_2[i][j])
        now_3.append(ss)
    now_2=now_3
    now_3=[]
    ind_for_del=[]
    for i in range(len(now_2)):
        for j in range(len(now_2)):
            if now_2[j]==now_2[i]:
                continue
            if now_2[j].count(now_2[i])>=1:
                ind_for_del.append(j)
    for i in range(len(now_2)):
        bol=True
        for j in range(len(ind_for_del)):
            if i==ind_for_del[j]:
                bol=False
        if bol:
            now_3.append(now_2[i])
    now_2=now_3
    now_3=[]
    bol=True
    for i in range(len(now_2)):
        for j in range(i+1,len(now_2)):
            if now_2[j]==now_2[i]:
                bol=False
        if bol:
            now_3.append(now_2[i])
    now_2=now_3
    return now_2
def delp(m):
    now_no_powt=[]
    for i in range(len(m)):
        now_no_powt.append("".join(set(m[i])))
    m=now_no_powt
    delo=[]
    for i in range(len(m)):
        for j in range(len(m)):
            if i==j:
                continue
            bol=True
            for dl in range(len(delo)):
                if i==delo[dl] or j==delo[dl]:
                bol=False
            if bol:
                bol=True
                for ind in range(len(m[i])):
                    if m[j].count(m[i][ind])==0:
                        bol=False 
                if bol:
                    delo.append(j)
    d=[]
    for i in range(len(m)):
        bol=True
        for j in range(len(delo)):
            if i==delo[j]:
                bol=False
        if bol:
            d.append(m[i])
    return d
start=[[1,1,1,0,1,0,1],[0,0,1,0,0,1,0],[1,1,0,1,0,0,1],[1,1,1,1,1,1,1],[1,1,1,1,1,0,1],[1,1,1,1,1,1,0]]
classes=[[0,1,2],[3,4],[5]]
testing=[1,1,0,0,1,0,0]
M=[]
Edi=[]
Ms=[]
for i in range(len(classes[0])):
    now=start[classes[0][i]]
    prov_class=1
    for j in range(len(classes[prov_class])):
        prov_string=start[classes[prov_class][j]]
        s_i=[]
        col=0
        for it in range(len(now)):
            if now[it]==prov_string[it]:
                s_i.append(0)
            else:
                s_i.append(1)
                col=col+1
        Edi.append(col)
        M.append(s_i)
        Ms.append(s_i)
    prov_class=prov_class+1
ind_for_del=[]
for i in range(len(Edi)-1):
    if ind_for_del!=[]:
        if i==ind_for_del[-1]:
            continue
    now=M[i]
    if M[i]==[]:
        ind_for_del.append(i)
        continue
    for xi in range(i+1,len(Edi)):
        nex=M[xi]
        col=0
        for j in range(len(M[i])):
            if now[j]==nex[j] and now[j]==1:
                col=col+1
                if col==Edi[i]:
                    ind_for_del.append(xi)
                    break
Mss=[]
for i in range(len(Ms)):
    bol=True
    for j in range(len(ind_for_del)):  
        if i==ind_for_del[j]:
            bol=False
            break
        bol=True
    if bol:
        Mss.append(Ms[i])
EP=[]
for i in range(len(Mss)):
    s_i=[]
    for j in range(len(Mss[i])):
        if Mss[i][j]==1:
            s_i.append(str(j+1))
    EP.append(s_i)
ind_for_del=[]
Epm=[]
col=0
bol=True
for i in range(len(EP)):
    if EP[i]==[]:
        bol=False
        col=col+1
if bol==False:
if bol:
    now=EP[0]
    for i in range(1,len(EP)):
        nex=EP[i]
        si=[]
        for j in range(len(now)):
            for xi in range(len(nex)):
                si.append(now[j]+nex[xi])
        now=delp(si)
    now_2=now
    count_P=[]
    for i in range(len(M[0])):
        col=0
        for j in range(len(now_2)):
            col=col+now_2[j].count(str(i+1))
        count_P.append(str(col)+'/'+str(len(now_2)))
    for i in range(len(count_P)):
        ss='P'+str(i)+'='+str(count_P[i])
            print(ss)
    in_class=[]
    for i in range(len(classes)):
        in_test=[0 for c in now_2]
        for j in range(len(classes[i])):
            tec=classes[i][j]
            for i_now_2 in range(len(now_2)):
                col_in=0
                bol=True
                for in_str in range(len(now_2[i_now_2])):
                    ind=int(now_2[i_now_2][in_str])-1
                    if testing[ind]!=start[tec][ind]:
                        bol=False
                if(bol):
                    col_in=col_in+1
                    in_test[i_now_2]=in_test[i_now_2]+1
        in_class.append(in_test)
    str_1='Termi'
    str_2='itogo'
    for i in range(len(in_class)):
        str_1=str_1+'\tClass'+str(i+1)
    print(str_1)
    c = ['0' for c in now_2]
    for i in range(len(now_2)):
        c[i]=now_2[i]
    for i in range(len(in_class)):
        for j in range(len(in_class[i])):
            c[j]=c[j]+'\t'+str(in_class[i][j])
    print_m(c)     
    col_in_cl=[]
    for i in range(len(in_class)):
        col=0
        for j in range(len(in_class[i])):
            col=col+in_class[i][j]
        col_in_cl.append(col)
        str_2=str_2+'\t'+str(col)
    print(str_2)    
    print('Test vib prin classy',col_in_cl.index(max(col_in_cl))+1)
