# Cloud monitor
Hej drengene.
Jeg har flikket en lille test ting sammen.
Jeg fik jo ikke lige koden, så jeg måtte lige lære lidt nødtørftig Python :-)
Bær over med det barnlige forsøg, kan ikke helt huske hvad felterne indeholder.

Men, byg image og spin den op.
Der kører nu et lille frækt site på [localhost:80](http://localhost:80)
Opret et par fake poster

Hvis i kører CLI: *(smid selv sudo på hvis i ikke kører på windoze som mig med linux i vm)*

Kald: `docker ps`
Den burde vise container der hedder cloudmonitor.
Kald `docker volume ls`
Der burde være en entry der hedder cloudmonitor_datavolume.

Nu skal vi lave sjov..
Kald `docker stop cloudmonitor && docker rm cloudmonitor`
Så er den slået ihjel og fjernet....

Prøv nu at bygge og kør den igen. Denne gang, hop over på listen på sitet og se om data stadig er der.

PRESTO!
Alt er godt, og i kan begynde med alt det andet sjove.

## Byg Image
`docker build -t cloudmonitor .`

## Spin hele balladen op
`docker run -v cloudmonitor_datavolume:/database -p 80:5000 --name cloudmonitor cloudmonitor`




## BONUS
Docker er jo sjovt, og docker kommandoer er også sjove, de er bare nederen at huske.
Hvad med lidt orchestration?
Dette kræver at der sammen med docker er installeret docker compose.
Undersøg ved at kalde `docker-compose version` (nok med noget sudo hos jer)

Hvis den er der, så vil den snedige fyr se at jeg har lagt en fil i root ved siden af Dockerfile, der hedder compose.yaml.
Den er smart.
Prøv at kalde `docker-compose up`

Den sørger så for at det hele kommer i luften og kører. 
Hvis der er fejl, så start lige med at fyre den her af:
`docker stop cloudmonitor && docker rm cloudmonitor && docker volume rm cloudmonitor_datavolume`
Det vi prøver her er: 
- stop container
- fjern den
- nuke den gamle volume

Hvis det ellers gik godt, så er det bare CTRL+C for at stoppe den (Your milage may vary)

Start den nu på den snedige måde: `docker-compose up -d`
Så kører den i baggrunden....
Kald `docker-compose down` for at flå det hele ned igen.

Er der spørgsmål, så er det bare at spørge. Jeg er her jo for det samme :-)
