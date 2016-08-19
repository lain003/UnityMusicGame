BEGIN;

create table tapnote(id integer primary key autoincrement,tag integer,start_time real);
create index tapnote_tag on tapnote(tag);
create index tapnote_starttime on tapnote(start_time);

create table holdnote(id integer primary key autoincrement,tag integer,start_time real ,end_time real);
create index holdnote_tag on holdnote(tag);
create index holdnote_starttime on holdnote(start_time);
create index holdnote_endtime on holdnote(end_time);

COMMIT;
