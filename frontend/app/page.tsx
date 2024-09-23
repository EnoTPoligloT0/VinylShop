import React from "react";
import {Hero} from "@/components";
import {Header} from "@/components";
import {Banner} from "@/components";

export default function Home() {
  return (
    <main className={"overflow-hidden"}>
        <Header/>
      <Hero/>
        <Banner/>
    </main>
  );
}
